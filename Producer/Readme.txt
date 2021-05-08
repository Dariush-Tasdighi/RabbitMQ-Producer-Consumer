--------------------------------------------------
Install-Package RabbitMQ.Client

https://www.cloudamqp.com/blog/part1-rabbitmq-for-beginners-what-is-rabbitmq.html
https://www.cloudamqp.com/blog/part4-rabbitmq-for-beginners-exchanges-routing-keys-bindings.html
--------------------------------------------------

--------------------------------------------------
https://www.rabbitmq.com/queues.html

Durable (the queue will survive a broker restart)
Exclusive (used by only one connection and the queue will be deleted when that connection closes)
Auto-delete (queue that has had at least one consumer is deleted when last consumer unsubscribes)
Arguments (optional; used by plugins and broker-specific features such as message TTL, queue length limit, etc)
--------------------------------------------------
