# Microservices Overview

---

## Order Service
Port: 5199

Endpoints:
- POST http://localhost:5199/api/orders/create  
  Body:
  {
    "customerName": "John Doe",
    "customerEmail": "johndoe@gmail.com",
    "companyEmail": "testcompoany@gmail.com",
    "productDetails": "Product details",
    "productId": "a3c9e845-9e4b-44b4-8db1-6fc3a4e25a76",
    "quantity": 2
  }

- GET http://localhost:5199/api/orders/{id}/status

Consumers:
- StockFailed  
- StockConfirmed

---

## Stock Service
Port: 5136

Endpoint:
- POST http://localhost:5136/api/stocks/add-stock  
  Body:
  {
    "productId": "a3c9e845-9e4b-44b4-8db1-6fc3a4e25a76",
    "quantity": 10
  }

Consumers:
- StockCheck

---

## Email Service
Worker service

Consumers:
- CompanyEmail  
- CustomerEmail  
- StockFailed

---

## Infrastructure
- PostgreSQL and RabbitMQ are containerized and managed via Docker  
- MassTransit is used as the messaging library, integrated with RabbitMQ

---

## Next Steps
- Implement Saga (Orchestrator) — Use a state machine to coordinate the order lifecycle across services  
- Apply Retry and Outbox Pattern — Ensure reliability and message consistency across distributed systems  
- Add Payment Service — Process and confirm payments as part of the order flow  
- Add Shipping Service — Handle shipping operations as part of the overall order workflow