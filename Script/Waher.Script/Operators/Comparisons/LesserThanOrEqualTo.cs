﻿using System;
using System.Collections.Generic;
using System.Text;
using Waher.Script.Abstraction.Elements;
using Waher.Script.Abstraction.Sets;
using Waher.Script.Exceptions;
using Waher.Script.Model;
using Waher.Script.Objects;

namespace Waher.Script.Operators.Comparisons
{
    /// <summary>
    /// Lesser Than Or Equal To.
    /// </summary>
    public class LesserThanOrEqualTo : BinaryScalarOperator
    {
        /// <summary>
        /// Lesser Than Or Equal To.
        /// </summary>
        /// <param name="Left">Left operand.</param>
        /// <param name="Right">Right operand.</param>
        /// <param name="Start">Start position in script expression.</param>
        /// <param name="Length">Length of expression covered by node.</param>
		/// <param name="Expression">Expression containing script.</param>
        public LesserThanOrEqualTo(ScriptNode Left, ScriptNode Right, int Start, int Length, Expression Expression)
            : base(Left, Right, Start, Length, Expression)
        {
        }

        /// <summary>
        /// Evaluates the operator on scalar operands.
        /// </summary>
        /// <param name="Left">Left value.</param>
        /// <param name="Right">Right value.</param>
        /// <param name="Variables">Variables collection.</param>
        /// <returns>Result</returns>
        public override IElement EvaluateScalar(IElement Left, IElement Right, Variables Variables)
        {
			if (!(Left.AssociatedSet is IOrderedSet S))
				throw new ScriptRuntimeException("Cannot compare operands.", this);

			if (S.Compare(Left, Right) <= 0)
                return BooleanValue.True;
            else
                return BooleanValue.False;
        }
    }
}
