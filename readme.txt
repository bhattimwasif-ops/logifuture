1- Introduce Idempptency key to prevent replay and double spend
2- Use Message Queues for eventual consistancy with other system
3- Add retry logic and resilience (Polly / circuit breaker)
4- Use RowVersion for optimistic concurrency
5- Add monitor, logging and structured matrics
6- Harden db constraints
7- Deploy behind load balancer


Please check sequence and architecure diagrams too.