# EasyWay

[![Build & Tests](https://github.com/StrategiCoding/EasyWay/actions/workflows/build-and-tests.yaml/badge.svg)](https://github.com/StrategiCoding/EasyWay/actions/workflows/build-and-tests.yaml)

## ✨ Features

### :gear: Main
- CQRS and DomainEvent handlers
- Predefined domain building blocks (Tactical Domain-Driven Desing)
- Automatic update of concurrency token in AggregateRoots
- Automatic sequential Guid generation (in application)
- Automatic publication of domain events
- Automatic approval of all changes in one transaction (Unit of Work Pattern)

### :globe_with_meridians: WebApi
- Automatic translation of an exception into a response
- Creating endpoints based on CQRS handlers (RPC Style)
