#!/usr/bin/env bash

cat << QUERYEND > 01-cls-thing.rq
PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>
PREFIX rdf: <http://www.w3.org/1999/02/22-rdf-syntax-ns#>
PREFIX owl: <http://www.w3.org/2002/07/owl#>

INSERT { 
GRAPH <http://industrialinference.com/inferred/> {
owl:Thing rdf:type owl:Class .
} } WHERE {


}
QUERYEND

cat << QUERYEND > 02-cls-nothing1.rq
PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>
PREFIX rdf: <http://www.w3.org/1999/02/22-rdf-syntax-ns#>
PREFIX owl: <http://www.w3.org/2002/07/owl#>

INSERT { 
GRAPH <http://industrialinference.com/inferred/> {
owl:Nothing rdf:type owl:Class .
} } WHERE {

}
QUERYEND

cat << QUERYEND > 03-cls-nothing2.rq
PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>
PREFIX rdf: <http://www.w3.org/1999/02/22-rdf-syntax-ns#>
PREFIX owl: <http://www.w3.org/2002/07/owl#>

INSERT { 
GRAPH <http://industrialinference.com/contradictions/> {
    ?x rdf:type owl:Nothing .
} } WHERE {
    ?x rdf:type owl:Nothing .
}
QUERYEND

cat << QUERYEND > 04-cls-int1.rq
PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>
PREFIX rdf: <http://www.w3.org/1999/02/22-rdf-syntax-ns#>
PREFIX owl: <http://www.w3.org/2002/07/owl#>

INSERT { 
GRAPH <http://industrialinference.com/inferred/> {
    ?y rdf:type ?c .
} } WHERE {

    ?c owl:intersectionOf ?x .	
    LIST[?x ?c1 ... ?cn]	
    ?y rdf:type ?c1 .	
    ?y rdf:type ?c2 .	
    ...	
    ?y rdf:type ?cn .	
}
QUERYEND

cat << QUERYEND > 05-cls-int2.rq
PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>
PREFIX rdf: <http://www.w3.org/1999/02/22-rdf-syntax-ns#>
PREFIX owl: <http://www.w3.org/2002/07/owl#>

INSERT { 
GRAPH <http://industrialinference.com/inferred/> {
    ?y rdf:type ?c1 .
    ?y rdf:type ?c2 .
    ?y rdf:type ?cn .
} } WHERE {
    ?c owl:intersectionOf ?x .	
    LIST[?x ?c1 ... ?cn]	
    ?y rdf:type ?c .
}
QUERYEND

cat << QUERYEND > 06-cls-uni.rq
PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>
PREFIX rdf: <http://www.w3.org/1999/02/22-rdf-syntax-ns#>
PREFIX owl: <http://www.w3.org/2002/07/owl#>

INSERT { 
GRAPH <http://industrialinference.com/inferred/> {
    ?y rdf:type ?c .
} } WHERE {
    ?c owl:unionOf ?x .	
    LIST[?x ?c1 ... ?cn]	
    ?y rdf:type ?ci .	
}
QUERYEND

cat << QUERYEND > 07-cls-com.rq
PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>
PREFIX rdf: <http://www.w3.org/1999/02/22-rdf-syntax-ns#>
PREFIX owl: <http://www.w3.org/2002/07/owl#>

INSERT { 
GRAPH <http://industrialinference.com/contradictions/> {
    ?c1 owl:complementOf ?c2 .
    ?x rdf:type ?c1 .	
    ?x rdf:type ?c2 .	
} } WHERE {
    ?c1 owl:complementOf ?c2 .
    ?x rdf:type ?c1 .	
    ?x rdf:type ?c2 .	
}
QUERYEND

cat << QUERYEND > 08-cls-svf1.rq
PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>
PREFIX rdf: <http://www.w3.org/1999/02/22-rdf-syntax-ns#>
PREFIX owl: <http://www.w3.org/2002/07/owl#>

INSERT { 
GRAPH <http://industrialinference.com/inferred/> {
    ?u rdf:type ?x .
} } WHERE {
    ?x owl:someValuesFrom ?y .	
    ?x owl:onProperty ?p .	
    ?u ?p ?v .	
    ?v rdf:type ?y .	
}
QUERYEND

cat << QUERYEND > 09-cls-svf2.rq
PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>
PREFIX rdf: <http://www.w3.org/1999/02/22-rdf-syntax-ns#>
PREFIX owl: <http://www.w3.org/2002/07/owl#>

INSERT { 
GRAPH <http://industrialinference.com/inferred/> {
    ?u rdf:type ?x .
} } WHERE {
    ?x owl:someValuesFrom owl:Thing .	
    ?x owl:onProperty ?p .	
    ?u ?p ?v .	
}
QUERYEND

cat << QUERYEND > 10-cls-avf.rq
PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>
PREFIX rdf: <http://www.w3.org/1999/02/22-rdf-syntax-ns#>
PREFIX owl: <http://www.w3.org/2002/07/owl#>

INSERT { 
GRAPH <http://industrialinference.com/inferred/> {
    ?v rdf:type ?y .
} } WHERE {
    ?x owl:allValuesFrom ?y .	
    ?x owl:onProperty ?p .	
    ?u rdf:type ?x .	
    ?u ?p ?v .	
}
QUERYEND

cat << QUERYEND > 11-cls-hv1.rq
PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>
PREFIX rdf: <http://www.w3.org/1999/02/22-rdf-syntax-ns#>
PREFIX owl: <http://www.w3.org/2002/07/owl#>

INSERT { 
GRAPH <http://industrialinference.com/inferred/> {
    ?u ?p ?y .
} } WHERE {
    ?x owl:hasValue ?y .	
    ?x owl:onProperty ?p .	
    ?u rdf:type ?x .	
}
QUERYEND

cat << QUERYEND > 12-cls-hv2.rq
PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>
PREFIX rdf: <http://www.w3.org/1999/02/22-rdf-syntax-ns#>
PREFIX owl: <http://www.w3.org/2002/07/owl#>

INSERT { 
GRAPH <http://industrialinference.com/inferred/> {
    ?u rdf:type ?x .
} } WHERE {
    ?x owl:hasValue ?y .	
    ?x owl:onProperty ?p .	
    ?u ?p ?y .	
}
QUERYEND

cat << QUERYEND > 13-cls-maxc1.rq
PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>
PREFIX rdf: <http://www.w3.org/1999/02/22-rdf-syntax-ns#>
PREFIX owl: <http://www.w3.org/2002/07/owl#>

INSERT { 
GRAPH <http://industrialinference.com/contradictions/> {
    ?x owl:maxCardinality "0"^^xsd:nonNegativeInteger .	
    ?x owl:onProperty ?p .	
    ?u rdf:type ?x .	
    ?u ?p ?y .	
} } WHERE {
    ?x owl:maxCardinality "0"^^xsd:nonNegativeInteger .	
    ?x owl:onProperty ?p .	
    ?u rdf:type ?x .	
    ?u ?p ?y .	
}
QUERYEND

cat << QUERYEND > 14-cls-maxc2.rq
PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>
PREFIX rdf: <http://www.w3.org/1999/02/22-rdf-syntax-ns#>
PREFIX owl: <http://www.w3.org/2002/07/owl#>

INSERT { 
GRAPH <http://industrialinference.com/inferred/> {
    ?y1 owl:sameAs ?y2 .
} } WHERE {
    ?x owl:maxCardinality "1"^^xsd:nonNegativeInteger .	
    ?x owl:onProperty ?p .	
    ?u rdf:type ?x .	
    ?u ?p ?y1 .	
    ?u ?p ?y2 .	
}
QUERYEND

cat << QUERYEND > 15-cls-maxqc1.rq
PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>
PREFIX rdf: <http://www.w3.org/1999/02/22-rdf-syntax-ns#>
PREFIX owl: <http://www.w3.org/2002/07/owl#>

INSERT { 
GRAPH <http://industrialinference.com/contradictions/> {
    ?x owl:maxQualifiedCardinality "0"^^xsd:nonNegativeInteger .
    ?x owl:onProperty ?p .	
    ?x owl:onClass ?c .	
    ?u rdf:type ?x .	
    ?u ?p ?y .	
    ?y rdf:type ?c .	
} } WHERE {
    ?x owl:maxQualifiedCardinality "0"^^xsd:nonNegativeInteger .
    ?x owl:onProperty ?p .	
    ?x owl:onClass ?c .	
    ?u rdf:type ?x .	
    ?u ?p ?y .	
    ?y rdf:type ?c .	
}
QUERYEND

cat << QUERYEND > 16-cls-maxqc2.rq
PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>
PREFIX rdf: <http://www.w3.org/1999/02/22-rdf-syntax-ns#>
PREFIX owl: <http://www.w3.org/2002/07/owl#>

INSERT { 
GRAPH <http://industrialinference.com/contradictions/> {
    ?x owl:maxQualifiedCardinality "0"^^xsd:nonNegativeInteger .
    ?x owl:onProperty ?p .	
    ?x owl:onClass owl:Thing .	
    ?u rdf:type ?x .	
    ?u ?p ?y .	
} } WHERE {
    ?x owl:maxQualifiedCardinality "0"^^xsd:nonNegativeInteger .
    ?x owl:onProperty ?p .	
    ?x owl:onClass owl:Thing .	
    ?u rdf:type ?x .	
    ?u ?p ?y .	
}
QUERYEND

cat << QUERYEND > 17-cls-maxqc3.rq
PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>
PREFIX rdf: <http://www.w3.org/1999/02/22-rdf-syntax-ns#>
PREFIX owl: <http://www.w3.org/2002/07/owl#>

INSERT { 
GRAPH <http://industrialinference.com/inferred/> {
    ?y1 owl:sameAs ?y2 .
} } WHERE {
    ?x owl:maxQualifiedCardinality "1"^^xsd:nonNegativeInteger .	
    ?x owl:onProperty ?p .	
    ?x owl:onClass ?c .	
    ?u rdf:type ?x .	
    ?u ?p ?y1 .	
    ?y1 rdf:type ?c .	
    ?u ?p ?y2 .	
    ?y2 rdf:type ?c .	
}
QUERYEND

cat << QUERYEND > 18-cls-maxqc4.rq
PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>
PREFIX rdf: <http://www.w3.org/1999/02/22-rdf-syntax-ns#>
PREFIX owl: <http://www.w3.org/2002/07/owl#>

INSERT { 
GRAPH <http://industrialinference.com/inferred/> {
    ?y1 owl:sameAs ?y2 .
} } WHERE {
    ?x owl:maxQualifiedCardinality "1"^^xsd:nonNegativeInteger .	
    ?x owl:onProperty ?p .	
    ?x owl:onClass owl:Thing .	
    ?u rdf:type ?x .	
    ?u ?p ?y1 .	
    ?u ?p ?y2 .	
}
QUERYEND

cat << QUERYEND > 19-cls-oo.rq
PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>
PREFIX rdf: <http://www.w3.org/1999/02/22-rdf-syntax-ns#>
PREFIX owl: <http://www.w3.org/2002/07/owl#>

INSERT { 
GRAPH <http://industrialinference.com/inferred/> {
    ?y1 rdf:type ?c .
    ?yn rdf:type ?c .
} } WHERE {
    ?c owl:oneOf ?x .	
    LIST[?x ?y1 ... ?yn]
}
QUERYEND
