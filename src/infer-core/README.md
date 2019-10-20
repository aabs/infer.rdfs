# Inference Engine

This is a simple inference engine, available as a CLI tool, that will materialise inferences on a remote triple store.

## Caveats

1. It currently only properly implements the entailments defined for RDFS in the [RDF 1.1 Semantics](https://www.w3.org/TR/rdf11-mt/#patterns-of-rdfs-entailment-informative).

## Inference Engine Operation

1. Pull down contents of the target graphs
2. pull in and parse contents of rules
3. 