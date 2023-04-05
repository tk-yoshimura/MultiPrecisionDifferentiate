# MultiPrecisionDifferentiate
 MultiPrecision Numerical Differentiation Implements

## Requirement
.NET 6.0

## Install

[Download DLL](https://github.com/tk-yoshimura/MultiPrecisionDifferentiate/releases)  
[Download Nuget](https://www.nuget.org/packages/tyoshimura.multiprecision.differentiate/)  

- Import MultiPrecision(https://github.com/tk-yoshimura/MultiPrecision)

## Usage
```csharp
for (int derivative = 0; derivative <= 64; derivative++) {
    MultiPrecision<Pow2.N8> y = CenteredIntwayDifferential<Pow2.N8>.Differentiate(MultiPrecision<Pow2.N8>.Exp, 0, derivative, 0.125);

    Console.WriteLine($"{derivative}\t{y}");
}
```

## Licence
[MIT](https://github.com/tk-yoshimura/MultiPrecisionDifferentiate/blob/main/LICENSE)

## Author

[T.Yoshimura](https://github.com/tk-yoshimura)
