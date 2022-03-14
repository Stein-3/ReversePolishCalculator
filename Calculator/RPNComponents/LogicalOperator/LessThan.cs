﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.RPNComponents.LogicalOperator
{
    internal class LessThan : LogicalOperatorBase
    {
        public static ICalculationTarget DefinitionInstance = new LessThan { IsDefinitionInstance = true };

        protected override string Name => ">";

        public override ICalculationTarget Compare(NumberTarget value1, NumberTarget value2) => ToNumberTarget(value1.ToDouble() > value2.ToDouble());

        protected override ICalculationTarget Create() => new LessThan();
    }
}
