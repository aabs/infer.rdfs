using System;
using System.Collections.Generic;
using System.Linq;
using VDS.RDF;
using VDS.RDF.Parsing;
using VDS.RDF.Query;
using VDS.RDF.Update;
using VDS.RDF.Update.Commands;

namespace Inference.Core.Inference
{
    public class LocalInferenceEngine : BaseInferenceEngine, IInferenceEngine
    {
        private readonly ISparqlQueryProcessor _queryProcessor;
        private readonly ISparqlUpdateProcessor _updateProcessor;

        public LocalInferenceEngine(ISparqlQueryProcessor queryProcessor, ISparqlUpdateProcessor updateProcessor)
        {
            _queryProcessor = queryProcessor;
            _updateProcessor = updateProcessor;
        }

        public void Infer(Uri sourceGraphUri, EntailmentRegime entailmentRegime = EntailmentRegime.RDFS)
        {
            // 1. Load Target Graph
            var g = LoadGraphLocally(sourceGraphUri);
            // 2. Load Rule Set
            var rules = LoadRules();
            // 4. While there are inferences to be made
            // 4.1. Materialise inferences from graph plus ruleset
            // 5. Push materialised inferences back
            throw new NotImplementedException();
        }

        private RuleMapping ConvertInsertCommandToRuleMapping(InsertCommand ic)
        {
            return new RuleMapping(ic.WherePattern.TriplePatterns, ic.InsertPattern.ChildGraphPatterns[0].TriplePatterns);
        }

        public IEnumerable<RuleMapping> LoadRules()
        {
            foreach (var rule in GetRulesForRegime(EntailmentRegime.RDFS, GetType().Assembly))
                if (!string.IsNullOrWhiteSpace(rule))
                {
                    var sps = new SparqlParameterizedString(rule);
                    var parser = new SparqlUpdateParser();
                    var r = parser.ParseFromString(sps);
                    return r.Commands
                        .Cast<InsertCommand>()
                        .Select(ConvertInsertCommandToRuleMapping);
                }
            return Array.Empty<RuleMapping>();
        }

        private IGraph LoadGraphLocally(Uri sourceGraphUri)
        {
            var queryString = new SparqlParameterizedString();
            queryString.Namespaces.AddNamespace("ex", new Uri("http://example.org/ns#"));
            queryString.CommandText = "CONSTRUCT WHERE { GRAPH ?srcGraph { ?s ?p ?o . } }";
            queryString.SetUri("srcGraph", sourceGraphUri);
            var parser = new SparqlQueryParser();
            var query = parser.ParseFromString(queryString);
            return _queryProcessor.ProcessQuery(query) as IGraph;
        }
    }
}