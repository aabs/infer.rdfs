using System;
using System.Collections.Generic;
using System.Text;
using VDS.RDF;
using VDS.RDF.Parsing;
using VDS.RDF.Query;
using VDS.RDF.Update;
using VDS.RDF.Update.Commands;

namespace infer_core.inference
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
            IGraph g = LoadGraphLocally(sourceGraphUri);
            // 2. Load Rule Set
            var rules = LoadRules();
            // 4. While there are inferences to be made
            // 4.1. Materialise inferences from graph plus ruleset
            // 5. Push materialised inferences back
            throw new NotImplementedException();
        }

        public IEnumerable<InsertCommand> LoadRules()
        {
            foreach (var rule in GetRulesForRegime(EntailmentRegime.RDFS, GetType().Assembly))
            {
                if (!string.IsNullOrWhiteSpace(rule))
                {
                    var sps = new SparqlParameterizedString(rule);
                    var parser = new SparqlUpdateParser();
                    SparqlUpdateCommandSet r = parser.ParseFromString(sps);
                    foreach (var c in r.Commands)
                    {
                        yield return (InsertCommand)c;
                    }
                }
            }
        }

        IGraph LoadGraphLocally(Uri sourceGraphUri)
        {
            SparqlParameterizedString queryString = new SparqlParameterizedString();
            queryString.Namespaces.AddNamespace("ex", new Uri("http://example.org/ns#"));
            queryString.CommandText = "CONSTRUCT WHERE { GRAPH ?srcGraph { ?s ?p ?o . } }";
            queryString.SetUri("srcGraph", sourceGraphUri);
            SparqlQueryParser parser = new SparqlQueryParser();
            SparqlQuery query = parser.ParseFromString(queryString);
            IGraph g = _queryProcessor.ProcessQuery(query) as IGraph;
            return g;
        }
    }
}