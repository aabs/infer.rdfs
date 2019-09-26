using System.IO;
using System.Reflection;
using VDS.RDF.Parsing;
using VDS.RDF.Query;
using VDS.RDF.Update;

namespace infer_core
{
    public class InferenceEngine : IInferenceEngine
    {
        private readonly ISparqlQueryProcessor _queryProcessor;
        private readonly ISparqlUpdateProcessor _updateProcessor;

        public InferenceEngine(ISparqlQueryProcessor queryProcessor, ISparqlUpdateProcessor updateProcessor)
        {
            this._queryProcessor = queryProcessor;
            this._updateProcessor = updateProcessor;
        }

        public void Infer()
        {
            Assembly assembly = GetType().Assembly;
            foreach (var ruleFile in assembly.GetManifestResourceNames())
            {
                using var reader = new StreamReader(assembly.GetManifestResourceStream(ruleFile));
                string queryString = reader.ReadToEnd();
                var sps = new SparqlParameterizedString(queryString);
                if (!string.IsNullOrWhiteSpace(queryString))
                {
                    var parser = new SparqlUpdateParser();
                    var query = parser.ParseFromString(sps);
                    query.Process(_updateProcessor);
                }
            }
        }
    }
}