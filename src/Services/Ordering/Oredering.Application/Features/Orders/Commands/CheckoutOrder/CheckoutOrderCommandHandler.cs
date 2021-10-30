using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Oredering.Application.Contracts.Infrastructure;
using Oredering.Application.Contracts.Persistence;
using Oredering.Application.Models;
using Oredering.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Oredering.Application.Features.Orders.Commands.CheckoutOrder
{
    public class CheckoutOrderCommandHandler : IRequestHandler<CheckoutOrderCommand, int>
    {
        private readonly IOrderRepository orderRepository;
        private readonly IMapper mapper;
        private readonly IEmailService emailService;
        private readonly ILogger<CheckoutOrderCommandHandler> logger;

        public CheckoutOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper, IEmailService emailService, ILogger<CheckoutOrderCommandHandler> logger)
        {
            this.orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<int> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
        {
            Order order = mapper.Map<Order>(request);
            Order newOrder = await orderRepository.AddAsync(order);

            logger.LogInformation($"Order {order.Id} is successfully created.");

            await SendMail(newOrder);

            return newOrder.Id;
        }

        private async Task SendMail(Order order)
        {
            Email email = new()
            {
                To = "yanivoren12@gmail.com",
                Body = $"Order was created.",
                Subject = "Order was created"
            };

            try
            {
                await emailService.SendEmail(email);
            }
            catch (Exception ex)
            {
                logger.LogError($"Order {order.Id} faild due to an error with the mail service: {ex.Message}");
            }
        }
    }
}
