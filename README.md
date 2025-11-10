# 💻 Inventario BAC - Prueba Técnica

Este repositorio contiene la solución desarrollada para la prueba técnica de **Inventario de Productos y Ventas** solicitada por **BAC CREDOMATIC El Salvador**.

La solución se divide en dos proyectos separados para mantener el desarrollo desacoplado:

1.  **Backend (API Rest):** Implementado con **.NET Core 8 Web API**.
2.  **Frontend (Web Application):** Implementado con **.NET Core 8 MVC** (Modelo-Vista-Controlador).

---

## 🚀 Estado de la Implementación del Backend (API)

El **Backend está prácticamente completo**, con una sólida arquitectura en capas y todas las rutas (Endpoints) necesarias implementadas.

### ✅ Funcionalidad Implementada en el Backend (API)

* **Arquitectura:** Sólida estructura en capas, uso de **Entity Framework Core**, **AutoMapper**, y **FluentValidation**.
* **Seguridad:** **Login** y **Registro** funcionales con **Hashing de Contraseñas** y **JWT** para **Rutas Protegidas**.
* **Inventario:** **CRUD Completo de Productos (API Lista y Probada en Swagger).**
* **Ventas:** Se creó el **Endpoint del API para procesar la Venta**, incluyendo la lógica para recibir el **Encabezado** y **Detalle** de la venta y realizar el guardado mediante el **Procedimiento Almacenado**.

### 📊 Base de Datos

Se crearon las siguientes tablas y se hizo uso de **Procedimientos Almacenados (SPs)**:

* `Productos`, `EncabezadoVentas`, `DetalleVentas`.

> **Nota:** Se adjunta el *script SQL* con la creación de las tablas y el SP para que puedan ser ejecutados en SQL Server.

---

## 🌐 Funcionalidad del Frontend (Web MVC)

El frontend es una aplicación web MVC que consume la API Rest del backend.

* **Autenticación:**
    * Flujos de **Login, Registro, y Logout** implementados.
    * **Control de Acceso:** Lógica para proteger vistas (ej. Productos) si el usuario no está autenticado, y para no mostrar Login/Registro si ya lo está.
    * **Validación:** Uso de validación en el cliente, complementada por la **respuesta de validación del backend**.
* **Vista de Productos:** Se implementó la vista para el **Listado** de productos.

---

## 🛑 Pendientes para la Finalización

Pido disculpas por no haber podido completar el 100% de la funcionalidad debido a **asuntos personales de fuerza mayor** que afectaron el tiempo dedicado a la prueba. He priorizado la estabilidad y seguridad de la arquitectura del Backend.

Las pendientes se centran en el desarrollo de la interfaz de usuario y los módulos finales:

### ❌ Funcionalidad Pendiente (Frontend y Reportes)

1.  **CRUD de Inventario (Productos):** La interfaz de usuario (Front-end) y la lógica de consumo para **Crear, Actualizar y Eliminar** productos.
2.  **Módulo de Venta (Front-end):**
    * Lógica para la **búsqueda de productos por código** en la interfaz.
    * Lógica para realizar **cálculos en pantalla** (IVA, totales) al agregar productos.
    * Consumo del **Endpoint de Venta** implementado en el API para finalizar la transacción.
3.  **Manejo de Errores y Notificaciones** para mejorar la experiencia del usuario.
4.  Generación de **Reportes PDF y Excel** de ventas realizadas.

## ✍️ Nota Final y Agradecimiento

Agradezco sinceramente su comprensión por la entrega de esta prueba con funcionalidad incompleta. Debido a **asuntos personales de fuerza mayor**  no logré finalizar todos los módulos del Frontend.

Mi enfoque se priorizó en construir una base sólida y segura en el **Backend**, donde se encuentran implementadas la arquitectura, la seguridad (JWT/Hashing), el **CRUD completo** y los **Endpoints de Venta**.

Espero que la calidad y la estructura de mi trabajo en el Backend sean consideradas. Estoy convencido de mi capacidad para finalizar rápidamente los módulos pendientes.

Muchas gracias por la oportunidad y su tiempo de revisión.