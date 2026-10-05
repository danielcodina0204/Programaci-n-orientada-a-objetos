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

## Integrantes

| Nombre | Usuario GitHub | Aporte |
|--------|----------------|--------|
| | | |
| | | |
| | | |
