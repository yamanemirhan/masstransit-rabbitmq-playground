-Order Service-
port: 5199
req: http://localhost:5199/api/orders/create
body: {
  "customerName": "John Doe",
  "customerEmail": "johndoe@gmail.com",
  "companyEmail": "testcompoany@gmail.com",
  "productDetails": "Product details",
  "productId": "a3c9e845-9e4b-44b4-8db1-6fc3a4e25a76",
  "quantity": 2
}
req: http://localhost:5199/api/orders/{id}/status
consumers: 
  - StockFailed
  - StockConfirmed

-Stock Service-
port: 5136
req: http://localhost:5136/api/stocks/add-stock
body: {
    "productId": "a3c9e845-9e4b-44b4-8db1-6fc3a4e25a76",
    "quantity": 10
}
consumers: 
  - StockCheck

-Email Service-
worker service
consumers: 
  - CompanyEmail
  - CustomerEmail
  - StockFailed

-Infrastructure-
PostgreSQL and RabbitMQ are containerized and managed via Docker
MassTransit is used as the messaging library, integrated with RabbitMQ

Next steps:
* Implement Saga (Orchestrator) — Use a state machine to coordinate the order lifecycle across services
* Apply Retry and Outbox Pattern — Ensure reliability and message consistency across distributed systems
* Add Shipping Service — Handle shipping operations as part of the overall order workflow