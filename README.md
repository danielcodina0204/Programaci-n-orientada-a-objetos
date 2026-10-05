# Sistema de Gestión y Renta de Vehículos

Aplicación de consola en **C# (.NET 8)** para administrar y rentar una flota heterogénea de vehículos (automóviles, camionetas y motocicletas), aplicando abstracción, encapsulamiento, herencia, polimorfismo e interfaces.

## Ejecución

```bash
cd GestionVehiculos
dotnet run
```

El programa inicia con 6 vehículos de prueba (AUT001, AUT002, CAM001, CAM002, MOT001, MOT002). Para iniciar con la flota vacía, comente `gestor.CargarDatosDePrueba();` en `Program.cs`.

## Estructura

```
GestionVehiculos/
├── Interfaces/
│   └── IMantenimiento.cs     → Contrato: RealizarMantenimiento(), ConsultarMantenimiento()
├── Modelos/
│   ├── Vehiculo.cs           → Clase abstracta base (implementa IMantenimiento)
│   ├── Automovil.cs          → Derivada
│   ├── Camioneta.cs          → Derivada
│   └── Motocicleta.cs        → Derivada
├── Servicios/
│   └── GestorFlota.cs        → Lógica de negocio sobre el arreglo Vehiculo[]
├── Program.cs                → Menú do-while + switch y lectura segura de datos
└── GestionVehiculos.csproj
```

## Diagrama de clases

```
        IMantenimiento  (Interfaz)
               ▲
               │ <<implements>>
           Vehiculo     (Clase Base Abstracta)
               │
   ┌───────────┼────────────┐
Automovil  Camioneta   Motocicleta
```

## Funcionalidades (menú)

| # | Opción | Implementación |
|---|--------|----------------|
| 1 | Registrar vehículo | Submenú por tipo; datos generales + específicos; se guarda en `Vehiculo[]`. Valida código único, año, km y precio. |
| 2 | Mostrar todos | `MostrarVehiculos()` → llamada polimórfica a `MostrarInformacion()` |
| 3 | Buscar por código | `Buscar(codigo)` con `Equals(..., StringComparison.OrdinalIgnoreCase)` |
| 4 | Disponibles | Sobrecarga `MostrarVehiculos("Disponible")` |
| 5 | Rentados | Sobrecarga `MostrarVehiculos("Rentado")` |
| 6 | Cambiar estado | Disponible / Rentado / En Mantenimiento, con reglas de negocio (no se renta un vehículo en mantenimiento; al devolver se actualiza el km) |
| 7 | Mayor kilometraje | Algoritmo de búsqueda del máximo |
| 8 | Precio promedio | Suma / cantidad |
| 9 | Conteo por tipo | Operador `is` + casting explícito `(Automovil)v`, etc. |
| 10 | Mantenimiento | Se usa una referencia `IMantenimiento` para consultar y realizar el mantenimiento |
| 11 | Salir | |

## Conceptos de POO aplicados

- **Abstracción:** `Vehiculo` es `abstract` y declara la propiedad abstracta `IntervaloMantenimientoKm`, que cada derivada define (auto 10.000 km, camioneta 8.000 km, moto 5.000 km).
- **Encapsulamiento:** `Tipo` y `RequiereMantenimiento` tienen `protected set`; `KilometrosUltimoMantenimiento` y `EvaluarDesgaste()` son `protected`.
- **Herencia:** constructores derivados que llaman a `: base(...)`.
- **Polimorfismo:** `override` de `MostrarInformacion()`, `RealizarMantenimiento()` y `ConsultarMantenimiento()` en cada derivada; procesamiento del arreglo `Vehiculo[]`.
- **Sobrecarga:** `Buscar()` / `Buscar(string)`, `MostrarVehiculos()` / `MostrarVehiculos(string)`, `LeerEntero(string)` / `LeerEntero(string, int, int)`.
- **Interfaces:** `IMantenimiento` implementada por `Vehiculo` y especializada en cada derivada.
- **Validación:** `int.TryParse` / `double.TryParse` (acepta coma o punto decimal), campos no vacíos, rangos y respuestas S/N.

## Marco conceptual

El diseño del sistema se apoya en los conceptos de Programación Orientada a Objetos (POO) trabajados en la bibliografía básica del curso (Oviedo Regino, 2015; Flórez Fernández, 2012). A continuación se define cada concepto y se indica cómo se aplicó en el proyecto.

### Clase y objeto
Una *clase* es una plantilla que describe los atributos (estado) y métodos (comportamiento) comunes a un conjunto de objetos; un *objeto* es una instancia concreta de esa clase.
- En el proyecto: Automovil, Camioneta y Motocicleta son clases; cada vehículo registrado (por ejemplo AUT001) es un objeto creado con new.

### Abstracción
Consiste en representar solo las características esenciales de una entidad del mundo real, ignorando los detalles irrelevantes. Una *clase abstracta* no puede instanciarse y sirve como base común para otras clases.
- En el proyecto: Vehiculo es abstract: reúne lo que todos los vehículos comparten (código, marca, modelo, año, estado, kilometraje, precio) y declara la propiedad abstracta IntervaloMantenimientoKm, que cada clase hija debe definir.

### Encapsulamiento
Es el ocultamiento del estado interno de un objeto, permitiendo el acceso solo a través de una interfaz controlada (propiedades y métodos) y mediante modificadores de acceso (public, protected, private).
- En el proyecto: Tipo y RequiereMantenimiento solo pueden modificarse desde la jerarquía (protected set); KilometrosUltimoMantenimiento y EvaluarDesgaste() son protected; el arreglo flota de GestorFlota es private y solo se manipula con sus métodos.

### Herencia
Mecanismo por el cual una clase (derivada o hija) adquiere los atributos y métodos de otra (base o padre), y puede agregar o especializar comportamiento. Representa la relación *"es un"*.
- En el proyecto: un Automovil es un Vehiculo. Las tres clases derivadas heredan de Vehiculo y su constructor invoca al de la clase base con : base(...).

### Polimorfismo
Capacidad de que un mismo mensaje produzca comportamientos distintos según el tipo real del objeto que lo recibe.
- *Sobrescritura (virtual / override):* MostrarInformacion(), RealizarMantenimiento() y ConsultarMantenimiento() se redefinen en cada clase hija. Al recorrer el arreglo Vehiculo[] y llamar v.MostrarInformacion(), cada objeto muestra sus propios detalles.
- *Sobrecarga:* métodos con el mismo nombre y distinta lista de parámetros: Buscar() / Buscar(string), MostrarVehiculos() / MostrarVehiculos(string), LeerEntero(string) / LeerEntero(string, int, int).
- *Verificación de tipo y casting:* en el conteo por tipo se usa v is Automovil y luego (Automovil)v para acceder a atributos propios de la subclase.

### Interfaz
Es un contrato que define qué métodos debe tener una clase, sin indicar cómo se implementan. Permite desacoplar comportamientos y que clases distintas compartan capacidades.
- En el proyecto: IMantenimiento define RealizarMantenimiento() y ConsultarMantenimiento(). Vehiculo la implementa y cada derivada la especializa. En la opción 10 el vehículo se manipula mediante una referencia IMantenimiento.

### Colecciones de objetos (arreglos polimórficos)
Un arreglo del tipo de la clase base puede almacenar objetos de cualquiera de sus clases derivadas.
- En el proyecto: Vehiculo[] flota = new Vehiculo[50] guarda automóviles, camionetas y motocicletas en la misma estructura.

## Referencias

- Oviedo Regino, E. (2015). Lógica de programación orientada a objetos. ECOE Ediciones. https://bibliotecadigital.ucc.edu.co/permalink/57UCC_INST/6jo20q/alma990001928060204416
- Flórez Fernández, H. A. (2012). Programación orientada a objetos usando Java. ECOE Ediciones. https://bibliotecadigital.ucc.edu.co/permalink/57UCC_INST/6jo20q/alma992013700204416

## Integrantes

| Nombre | Usuario GitHub | Aporte |
|--------|----------------|--------|
| Daniel Codina | [@danielcodina0204](https://github.com/danielcodina0204) | Estructura inicial del proyecto, implementación de `IMantenimiento` y desarrollo de `Vehiculo`. |
| Juan Pablo Gutierrez | [@wosito215](https://github.com/wosito215) | Desarrollo de `Automovil`, `Camioneta` y `Motocicleta`. |
| Moises Zambrano | [@LordMoises-123](https://github.com/LordMoises-123) | Desarrollo del menú y validaciones en `Program`. |
| Omar Escorcia | [@OmarHwga](https://github.com/OmarHwga) | Desarrollo de `GestorFlota` en 3 etapas: registro y listados; búsqueda, estados y estadísticas; y mantenimiento. |

