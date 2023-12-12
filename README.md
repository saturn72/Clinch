some points:

- formulas may contain constants and/or cell index:
  - Set("d1", "=123")
  - Set("d1", "=a1+123")
  - Set("d1", "=a1/a2")

-  formulas may also use sub-formulas:
  - Set("d1", "=c1*c3")
  - Set("d2", "=d1-136") // d1 is sub formula

- cell value is calculated when required
- the `AritmeticOperation` data structure is not required for the task, but I created it to support future optimization (thought we would discuss the code, see below)
- suggested optimization: caluculate the value of formula-based-cell when is set.
