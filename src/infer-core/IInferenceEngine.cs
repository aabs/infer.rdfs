using System;

namespace Inference.Core
{
    public enum EntailmentRegime
    {
        RDFS, RDFSPLUS, OWL2EL, OWL2QL, OWL2RL, OWL2FULL
    }

    public interface IInferenceEngine
    {
        void Infer(Uri sourceGraphUri, EntailmentRegime entailmentRegime = EntailmentRegime.RDFS);
    }
}