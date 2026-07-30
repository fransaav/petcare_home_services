# Architecture Review: PetCare Home Services

## 1. Análisis del Monolito Inicial
El sistema actual está estructurado como un Monolito en Capas (Layered Architecture), separando el código por preocupaciones técnicas (`API`, `Application`, `Domain`, `Infrastructure`).

**Problemas Identificados y Riesgos:**
- **Acoplamiento Fuerte en la Persistencia:** Existe un único `PetCareDbContext` que centraliza todas las entidades (Customers, Pets, Providers, Bookings, Payments). Esto crea un único punto de fallo, un cuello de botella para cambios concurrentes, e impide que los módulos evolucionen independientemente.
- **Ausencia de Límites de Dominio (Bounded Contexts):** Las capas técnicas no reflejan el negocio. Cualquier cambio en la lógica de un área puede afectar inadvertidamente a otras, ya que todo comparte el mismo dominio y la misma infraestructura.
- **Riesgos de Mantenimiento:** A medida que el equipo y el sistema crezcan, la falta de barreras físicas (en código) fomentará un modelo de "bola de lodo" (Big Ball of Mud), donde las reglas de negocio de Facturación terminen acopladas a la estructura de Mascotas de formas imprevistas.

## 2. Justificación hacia un Monolito Modular
Para mitigar estos riesgos sin incurrir en la complejidad operativa de los microservicios, la solución idónea es adoptar una arquitectura de **Monolito Modular**, organizada por Contextos Delimitados (Bounded Contexts):
- **Cohesión Alta y Bajo Acoplamiento:** Agrupar el código por contexto de negocio (`IdentityAndPets`, `Booking`, `Providers`, `Billing`). Cada módulo contiene su propia capa de Dominio, Aplicación e Infraestructura.
- **Aislamiento de Datos:** Cada módulo gestionará su propia persistencia (DbContexts separados), garantizando que un módulo no pueda manipular directamente las tablas de otro, forzando la comunicación a través de interfaces formales.
- **Evolución Preparada:** Si en el futuro un módulo (ej. `Booking`) necesita escalar de forma independiente o ser reescrito, su extracción hacia un microservicio será mucho más directa, pues sus límites arquitectónicos y de datos ya están aislados.
