Next improvements...
## Priority:
1. Create a maintenance for `payslip variables` like `GrossPay`, `NetPay`, etc. and tag it if it is an Earning or Deduction.
2. Create a maintenance for `paylip variable formulas` like `NetPay = Earnings - Deductions`, tag `[A]` for constant formulas.
2. Create a maintenance for the `sequence` of `payslip variables`. `Sequence` is based on the computation of payroll which means `sequenceIds` for earnings comes first followed by deductions. Tag if the `payslip variable` is `earning` or `deduction`.
3. Create a maintenance for government deductions like `SSS, PhilHealth, PAGibig, TAX, etc.`.
4. Create a maintenance for `payroll cut-offs`.
5. Create a maintenance for `employee active rates`.
6. Create a maintenance for `earnings`.
7. Create a maintenance for `deductions`.
8. Create a maintenance for `branch, departments`.
9. Update the `employee table`, add some secondary information such as `branch, department, SSS #, PhilHealth #, PAGibig #`, etc.


## Trivial:
1. Add information / error logging.
2. Add swagger for API testing.
3. Add memory caching.