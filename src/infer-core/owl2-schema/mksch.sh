#!/usr/bin/env bash

cat << QUERYEND > 01-scm-cls.rq
PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>
PREFIX rdf: <http://www.w3.org/1999/02/22-rdf-syntax-ns#>
PREFIX owl: <http://www.w3.org/2002/07/owl#>

INSERT { 
GRAPH <http://industrialinference.com/inferred/> {
    ?c rdfs:subClassOf ?c .
} } WHERE {
    ?c rdf:type owl:Class .
    ?c owl:equivalentClass ?c .
    ?c rdfs:subClassOf owl:Thing .
    owl:Nothing rdfs:subClassOf ?c .
}
QUERYEND

cat << QUERYEND > 02-scm-sco.rq
PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>
PREFIX rdf: <http://www.w3.org/1999/02/22-rdf-syntax-ns#>
PREFIX owl: <http://www.w3.org/2002/07/owl#>

INSERT { 
GRAPH <http://industrialinference.com/inferred/> {
    ?c1 rdfs:subClassOf ?c3 .
} } WHERE {
    ?c1 rdfs:subClassOf ?c2 .
    ?c2 rdfs:subClassOf ?c3 .
}
QUERYEND

cat << QUERYEND > 03-scm-eqc1.rq
PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>
PREFIX rdf: <http://www.w3.org/1999/02/22-rdf-syntax-ns#>
PREFIX owl: <http://www.w3.org/2002/07/owl#>

INSERT { 
GRAPH <http://industrialinference.com/inferred/> {
    ?c1 rdfs:subClassOf ?c2 .
} } WHERE {
    ?c1 owl:equivalentClass ?c2 .
    ?c2 rdfs:subClassOf ?c1 .
}
QUERYEND

cat << QUERYEND > 04-scm-eqc2.rq
PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>
PREFIX rdf: <http://www.w3.org/1999/02/22-rdf-syntax-ns#>
PREFIX owl: <http://www.w3.org/2002/07/owl#>

INSERT { 
GRAPH <http://industrialinference.com/inferred/> {
    ?c1 owl:equivalentClass ?c2 .
} } WHERE {
    ?c1 rdfs:subClassOf ?c2 .
    ?c2 rdfs:subClassOf ?c1 .
}
QUERYEND

cat << QUERYEND > 05-scm-op.rq
PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>
PREFIX rdf: <http://www.w3.org/1999/02/22-rdf-syntax-ns#>
PREFIX owl: <http://www.w3.org/2002/07/owl#>

INSERT { 
GRAPH <http://industrialinference.com/inferred/> {
    ?p rdfs:subPropertyOf ?p .
} } WHERE {
    ?p rdf:type owl:ObjectProperty .
    ?p owl:equivalentProperty ?p .
}
QUERYEND

cat << QUERYEND > 06-scm-dp.rq
PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>
PREFIX rdf: <http://www.w3.org/1999/02/22-rdf-syntax-ns#>
PREFIX owl: <http://www.w3.org/2002/07/owl#>

INSERT { 
GRAPH <http://industrialinference.com/inferred/> {
    ?p rdfs:subPropertyOf ?p .
} } WHERE {
    ?p rdf:type owl:DatatypeProperty .
    ?p owl:equivalentProperty ?p .
}
QUERYEND

cat << QUERYEND > 07-scm-spo.rq
PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>
PREFIX rdf: <http://www.w3.org/1999/02/22-rdf-syntax-ns#>
PREFIX owl: <http://www.w3.org/2002/07/owl#>

INSERT { 
GRAPH <http://industrialinference.com/inferred/> {
    ?p1 rdfs:subPropertyOf ?p3 .
} } WHERE {
    ?p1 rdfs:subPropertyOf ?p2 .
    ?p2 rdfs:subPropertyOf ?p3 .
}
QUERYEND

cat << QUERYEND > 08-scm-eqp1.rq
PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>
PREFIX rdf: <http://www.w3.org/1999/02/22-rdf-syntax-ns#>
PREFIX owl: <http://www.w3.org/2002/07/owl#>

INSERT { 
GRAPH <http://industrialinference.com/inferred/> {
    ?p1 rdfs:subPropertyOf ?p2 .
} } WHERE {
    ?p1 owl:equivalentProperty ?p2 .
    ?p2 rdfs:subPropertyOf ?p1 .
}
QUERYEND

cat << QUERYEND > 09-scm-eqp2.rq
PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>
PREFIX rdf: <http://www.w3.org/1999/02/22-rdf-syntax-ns#>
PREFIX owl: <http://www.w3.org/2002/07/owl#>

INSERT { 
GRAPH <http://industrialinference.com/inferred/> {
    ?p1 owl:equivalentProperty ?p2 .
} } WHERE {
    ?p1 rdfs:subPropertyOf ?p2 .
    ?p2 rdfs:subPropertyOf ?p1 .
}
QUERYEND

cat << QUERYEND > 10-scm-dom1.rq
PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>
PREFIX rdf: <http://www.w3.org/1999/02/22-rdf-syntax-ns#>
PREFIX owl: <http://www.w3.org/2002/07/owl#>

INSERT { 
GRAPH <http://industrialinference.com/inferred/> {
    ?p rdfs:domain ?c2 .
} } WHERE {
    ?p rdfs:domain ?c1 .
    ?c1 rdfs:subClassOf ?c2 .
}
QUERYEND

cat << QUERYEND > 11-scm-dom2.rq
PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>
PREFIX rdf: <http://www.w3.org/1999/02/22-rdf-syntax-ns#>
PREFIX owl: <http://www.w3.org/2002/07/owl#>

INSERT { 
GRAPH <http://industrialinference.com/inferred/> {
    ?p1 rdfs:domain ?c .
} } WHERE {
    ?p2 rdfs:domain ?c .
    ?p1 rdfs:subPropertyOf ?p2 .
}
QUERYEND

cat << QUERYEND > 12-scm-rng1.rq
PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>
PREFIX rdf: <http://www.w3.org/1999/02/22-rdf-syntax-ns#>
PREFIX owl: <http://www.w3.org/2002/07/owl#>

INSERT { 
GRAPH <http://industrialinference.com/inferred/> {
    ?p rdfs:range ?c2 .
} } WHERE {
    ?p rdfs:range ?c1 .
    ?c1 rdfs:subClassOf ?c2 .
}
QUERYEND

cat << QUERYEND > 13-scm-rng2.rq
PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>
PREFIX rdf: <http://www.w3.org/1999/02/22-rdf-syntax-ns#>
PREFIX owl: <http://www.w3.org/2002/07/owl#>

INSERT { 
GRAPH <http://industrialinference.com/inferred/> {
    ?p1 rdfs:range ?c .
} } WHERE {
    ?p2 rdfs:range ?c .
    ?p1 rdfs:subPropertyOf ?p2 .
}
QUERYEND

cat << QUERYEND > 14-scm-hv.rq
PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>
PREFIX rdf: <http://www.w3.org/1999/02/22-rdf-syntax-ns#>
PREFIX owl: <http://www.w3.org/2002/07/owl#>

INSERT { 
GRAPH <http://industrialinference.com/inferred/> {
    ?c1 rdfs:subClassOf ?c2 .
} } WHERE {
    ?c1 owl:hasValue ?i .
    ?c1 owl:onProperty ?p1 .
    ?c2 owl:hasValue ?i .
    ?c2 owl:onProperty ?p2 .
    ?p1 rdfs:subPropertyOf ?p2 .
}
QUERYEND

cat << QUERYEND > 15-scm-svf1.rq
PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>
PREFIX rdf: <http://www.w3.org/1999/02/22-rdf-syntax-ns#>
PREFIX owl: <http://www.w3.org/2002/07/owl#>

INSERT { 
GRAPH <http://industrialinference.com/inferred/> {
    ?c1 rdfs:subClassOf ?c2 .
} } WHERE {
    ?c1 owl:someValuesFrom ?y1 .
    ?c1 owl:onProperty ?p .
    ?c2 owl:someValuesFrom ?y2 .
    ?c2 owl:onProperty ?p .
    ?y1 rdfs:subClassOf ?y2 .
}
QUERYEND

cat << QUERYEND > 16-scm-svf2.rq
PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>
PREFIX rdf: <http://www.w3.org/1999/02/22-rdf-syntax-ns#>
PREFIX owl: <http://www.w3.org/2002/07/owl#>

INSERT { 
GRAPH <http://industrialinference.com/inferred/> {
    ?c1 rdfs:subClassOf ?c2 .
} } WHERE {
    ?c1 owl:someValuesFrom ?y .
    ?c1 owl:onProperty ?p1 .
    ?c2 owl:someValuesFrom ?y .
    ?c2 owl:onProperty ?p2 .
    ?p1 rdfs:subPropertyOf ?p2 .
}
QUERYEND

cat << QUERYEND > 17-scm-avf1.rq
PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>
PREFIX rdf: <http://www.w3.org/1999/02/22-rdf-syntax-ns#>
PREFIX owl: <http://www.w3.org/2002/07/owl#>

INSERT { 
GRAPH <http://industrialinference.com/inferred/> {
    ?c1 rdfs:subClassOf ?c2 .
} } WHERE {
    ?c1 owl:allValuesFrom ?y1 .
    ?c1 owl:onProperty ?p .
    ?c2 owl:allValuesFrom ?y2 .
    ?c2 owl:onProperty ?p .
    ?y1 rdfs:subClassOf ?y2 .
}
QUERYEND

cat << QUERYEND > 18-scm-avf2.rq
PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>
PREFIX rdf: <http://www.w3.org/1999/02/22-rdf-syntax-ns#>
PREFIX owl: <http://www.w3.org/2002/07/owl#>

INSERT { 
GRAPH <http://industrialinference.com/inferred/> {
    ?c2 rdfs:subClassOf ?c1 .
} } WHERE {
    ?c1 owl:allValuesFrom ?y .
    ?c1 owl:onProperty ?p1 .
    ?c2 owl:allValuesFrom ?y .
    ?c2 owl:onProperty ?p2 .
    ?p1 rdfs:subPropertyOf ?p2 .
}
QUERYEND

cat << QUERYEND > 19-scm-int.rq
PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>
PREFIX rdf: <http://www.w3.org/1999/02/22-rdf-syntax-ns#>
PREFIX owl: <http://www.w3.org/2002/07/owl#>

INSERT { 
GRAPH <http://industrialinference.com/inferred/> {
    ?c rdfs:subClassOf ?c1 .
    ?c rdfs:subClassOf ?c2 .
    ?c rdfs:subClassOf ?cn .
} } WHERE {
    ?c owl:intersectionOf ?x .
    LIST[?x ?c1 ... ?cn]	
}
QUERYEND

cat << QUERYEND > 20-scm-uni.rq
PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>
PREFIX rdf: <http://www.w3.org/1999/02/22-rdf-syntax-ns#>
PREFIX owl: <http://www.w3.org/2002/07/owl#>

INSERT { 
GRAPH <http://industrialinference.com/inferred/> {
    ?c1 rdfs:subClassOf ?c .
    ?c2 rdfs:subClassOf ?c .
    ?cn rdfs:subClassOf ?c .
} } WHERE {
    ?c owl:unionOf ?x .
    LIST[?x ?c1 ... ?cn]	
}
QUERYEND
