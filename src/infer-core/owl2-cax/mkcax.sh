#!/usr/bin/env bash

cat << QUERYEND > 01-cax-sco.rq
PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>
PREFIX rdf: <http://www.w3.org/1999/02/22-rdf-syntax-ns#>
PREFIX owl: <http://www.w3.org/2002/07/owl#>

INSERT { 
GRAPH <http://industrialinference.com/inferred/> {
    ?x rdf:type ?c2 .
} } WHERE {
    ?c1 rdfs:subClassOf ?c2 .
    ?x rdf:type ?c1 .
}
QUERYEND

cat << QUERYEND > 02-cax-eqc1.rq
PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>
PREFIX rdf: <http://www.w3.org/1999/02/22-rdf-syntax-ns#>
PREFIX owl: <http://www.w3.org/2002/07/owl#>

INSERT { 
GRAPH <http://industrialinference.com/inferred/> {
    ?x rdf:type ?c2 .
} } WHERE {
    ?c1 owl:equivalentClass ?c2 .
    ?x rdf:type ?c1 .
}
QUERYEND

cat << QUERYEND > 03-cax-eqc2.rq
PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>
PREFIX rdf: <http://www.w3.org/1999/02/22-rdf-syntax-ns#>
PREFIX owl: <http://www.w3.org/2002/07/owl#>

INSERT { 
GRAPH <http://industrialinference.com/inferred/> {
    ?x rdf:type ?c1 .
} } WHERE {
    ?c1 owl:equivalentClass ?c2 .
    ?x rdf:type ?c2 .
}
QUERYEND

cat << QUERYEND > 04-cax-dw.rq
PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>
PREFIX rdf: <http://www.w3.org/1999/02/22-rdf-syntax-ns#>
PREFIX owl: <http://www.w3.org/2002/07/owl#>

INSERT { 
GRAPH <http://industrialinference.com/contradictions/> {
    ?c1 owl:disjointWith ?c2 .
    ?x rdf:type ?c1 .
    ?x rdf:type ?c2 .
} } WHERE {
    ?c1 owl:disjointWith ?c2 .
    ?x rdf:type ?c1 .
    ?x rdf:type ?c2 .
}
QUERYEND

cat << QUERYEND > 05-cax-adc.rq
PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>
PREFIX rdf: <http://www.w3.org/1999/02/22-rdf-syntax-ns#>
PREFIX owl: <http://www.w3.org/2002/07/owl#>

INSERT { 
GRAPH <http://industrialinference.com/contradictions/> {
    ?x rdf:type owl:AllDisjointClasses .
    ?x owl:members ?y .
    LIST[?y ?c1 ... ?cn]	
    ?z rdf:type ?ci .
    ?z rdf:type ?cj .
} } WHERE {
    ?x rdf:type owl:AllDisjointClasses .
    ?x owl:members ?y .
    LIST[?y ?c1 ... ?cn]	
    ?z rdf:type ?ci .
    ?z rdf:type ?cj .
}
QUERYEND

