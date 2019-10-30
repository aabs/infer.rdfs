using System;
using System.Collections.Generic;
using System.Text;
using VDS.RDF.Query.Patterns;

namespace infer_core.inference
{
    /// <summary>
    /// A class to capture the essentials of an inference rule
    /// </summary>
    public class RuleMapping
    {
        public List<ITriplePattern> Antecedents { get; }
        public List<ITriplePattern> Consequent { get; }

        public RuleMapping(List<ITriplePattern> antecedents, List<ITriplePattern> consequent)
        {
            Antecedents = antecedents;
            Consequent = consequent;
        }
    }
}
