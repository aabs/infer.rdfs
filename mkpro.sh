#!/usr/bin/env bash

echo  << EOF > 01-prp-dom.rq
    PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>
    PREFIX rdf: <http://www.w3.org/1999/02/22-rdf-syntax-ns#>
    PREFIX owl: <http://www.w3.org/2002/07/owl#>

    INSERT { 
    GRAPH <http://industrialinference.com/inferred/> {
        ?x rdf:type ?c .
    } } WHERE {
        ?p rdfs:domain ?c .
        ?x ?p ?y .
    } 
EOF

echo  << EOF > 02-prp-rng.rq
    PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>
    PREFIX rdf: <http://www.w3.org/1999/02/22-rdf-syntax-ns#>
    PREFIX owl: <http://www.w3.org/2002/07/owl#>

    INSERT { 
    GRAPH <http://industrialinference.com/inferred/> {
        ?y rdf:type ?c .
    } } WHERE {
        ?p rdfs:range ?c .
        ?x ?p ?y .
    } 
 
EOF

echo  << EOF > 03-prp-fp.rq
    PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>
    PREFIX rdf: <http://www.w3.org/1999/02/22-rdf-syntax-ns#>
    PREFIX owl: <http://www.w3.org/2002/07/owl#>

    INSERT { 
    GRAPH <http://industrialinference.com/inferred/> {
        ?y1 owl:sameAs ?y2 .
    } } WHERE {
        ?p rdf:type owl:FunctionalProperty .
        ?x ?p ?y1 .
        ?x ?p ?y2 .
    } 

EOF

echo  << EOF > 04-prp-ifp.rq
    PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>
    PREFIX rdf: <http://www.w3.org/1999/02/22-rdf-syntax-ns#>
    PREFIX owl: <http://www.w3.org/2002/07/owl#>

    INSERT { 
    GRAPH <http://industrialinference.com/inferred/> {
        ?p rdf:type owl:InverseFunctionalProperty .
        ?x1 ?p ?y .
        ?x2 ?p ?y .
    } } WHERE {
        ?x1 owl:sameAs ?x2 .
    } 

EOF

echo  << EOF > 05-prp-irp.rq
    PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>
    PREFIX rdf: <http://www.w3.org/1999/02/22-rdf-syntax-ns#>
    PREFIX owl: <http://www.w3.org/2002/07/owl#>

    INSERT { 
    GRAPH <http://industrialinference.com/inferred/> {
        FALSE
    } } WHERE {
        ?p rdf:type owl:IrreflexiveProperty .
        ?x ?p ?x .
    } 

EOF

echo  << EOF > 06-prp-symp.rq
    PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>
    PREFIX rdf: <http://www.w3.org/1999/02/22-rdf-syntax-ns#>
    PREFIX owl: <http://www.w3.org/2002/07/owl#>

    INSERT { 
    GRAPH <http://industrialinference.com/inferred/> {
        ?y ?p ?x .
    } } WHERE {
        ?p rdf:type owl:SymmetricProperty .
        ?x ?p ?y .
    } 

EOF

echo  << EOF > 07-prp-asyp.rq
    PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>
    PREFIX rdf: <http://www.w3.org/1999/02/22-rdf-syntax-ns#>
    PREFIX owl: <http://www.w3.org/2002/07/owl#>

    INSERT { 
    GRAPH <http://industrialinference.com/inferred/> {
        FALSE
    } } WHERE {
        ?p rdf:type owl:AsymmetricProperty .
        ?x ?p ?y .
        ?y ?p ?x .
    } 

EOF

echo  << EOF > 08-prp-trp.rq
    PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>
    PREFIX rdf: <http://www.w3.org/1999/02/22-rdf-syntax-ns#>
    PREFIX owl: <http://www.w3.org/2002/07/owl#>

    INSERT { 
    GRAPH <http://industrialinference.com/inferred/> {
?x ?p ?z .
    } } WHERE {
?p rdf:type owl:TransitiveProperty .
?x ?p ?y .
?y ?p ?z .
    } 
 

EOF

echo  << EOF > 09-prp-spo1.rq
    PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>
    PREFIX rdf: <http://www.w3.org/1999/02/22-rdf-syntax-ns#>
    PREFIX owl: <http://www.w3.org/2002/07/owl#>

    INSERT { 
    GRAPH <http://industrialinference.com/inferred/> {
?x ?p2 ?y .
    } } WHERE {
?p1 rdfs:subPropertyOf ?p2 .
?x ?p1 ?y .
    } 

EOF

echo  << EOF > 10-prp-spo2.rq
    PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>
    PREFIX rdf: <http://www.w3.org/1999/02/22-rdf-syntax-ns#>
    PREFIX owl: <http://www.w3.org/2002/07/owl#>

    INSERT { 
    GRAPH <http://industrialinference.com/inferred/> {
?u1 ?p ?un+1 .
    } } WHERE {
?p owl:propertyChainAxiom ?x .
LIST[?x ?p1 ... ?pn]	
?u1 ?p1 ?u2 .
?u2 ?p2 ?u3 .
...	
?un ?pn ?un+1 .
    } 

EOF

echo  << EOF > 11-prp-eqp1.rq
    PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>
    PREFIX rdf: <http://www.w3.org/1999/02/22-rdf-syntax-ns#>
    PREFIX owl: <http://www.w3.org/2002/07/owl#>

    INSERT { 
    GRAPH <http://industrialinference.com/inferred/> {
?x ?p2 ?y .
    } } WHERE {
?p1 owl:equivalentProperty ?p2 .
?x ?p1 ?y .
    } 

EOF

echo  << EOF > 12-prp-eqp2.rq
    PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>
    PREFIX rdf: <http://www.w3.org/1999/02/22-rdf-syntax-ns#>
    PREFIX owl: <http://www.w3.org/2002/07/owl#>

    INSERT { 
    GRAPH <http://industrialinference.com/inferred/> {
?x ?p1 ?y .
    } } WHERE {
?p1 owl:equivalentProperty ?p2 .
?x ?p2 ?y .
    } 

EOF

echo  << EOF > 13-prp-pdw.rq
    PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>
    PREFIX rdf: <http://www.w3.org/1999/02/22-rdf-syntax-ns#>
    PREFIX owl: <http://www.w3.org/2002/07/owl#>

    INSERT { 
    GRAPH <http://industrialinference.com/inferred/> {
FALSE
    } } WHERE {
?p1 owl:propertyDisjointWith ?p2 .
?x ?p1 ?y .
?x ?p2 ?y .
    } 

EOF

echo  << EOF > 14-prp-adp.rq
    PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>
    PREFIX rdf: <http://www.w3.org/1999/02/22-rdf-syntax-ns#>
    PREFIX owl: <http://www.w3.org/2002/07/owl#>

    INSERT { 
    GRAPH <http://industrialinference.com/inferred/> {
FALSE
    } } WHERE {
?x rdf:type owl:AllDisjointProperties .
?x owl:members ?y .
LIST[?y ?p1 ... ?pn]	
?u ?pi ?v .
?u ?pj ?v .
    } 

EOF

echo  << EOF > 15-prp-inv1.rq
    PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>
    PREFIX rdf: <http://www.w3.org/1999/02/22-rdf-syntax-ns#>
    PREFIX owl: <http://www.w3.org/2002/07/owl#>

    INSERT { 
    GRAPH <http://industrialinference.com/inferred/> {
?y ?p2 ?x .
    } } WHERE {
?p1 owl:inverseOf ?p2 .
?x ?p1 ?y .
    } 

EOF

echo  << EOF > 16-prp-inv2.rq
    PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>
    PREFIX rdf: <http://www.w3.org/1999/02/22-rdf-syntax-ns#>
    PREFIX owl: <http://www.w3.org/2002/07/owl#>

    INSERT { 
    GRAPH <http://industrialinference.com/inferred/> {
?y ?p1 ?x .
    } } WHERE {
?p1 owl:inverseOf ?p2 .
?x ?p2 ?y .
    } 

EOF

echo  << EOF > 17-prp-key.rq
    PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>
    PREFIX rdf: <http://www.w3.org/1999/02/22-rdf-syntax-ns#>
    PREFIX owl: <http://www.w3.org/2002/07/owl#>

    INSERT { 
    GRAPH <http://industrialinference.com/inferred/> {
?x owl:sameAs ?y .
    } } WHERE {
?c owl:hasKey ?u .
LIST[?u ?p1 ... ?pn]	
?x rdf:type ?c .
?x ?p1 ?z1 .
...	
?x ?pn ?zn .
?y rdf:type ?c .
?y ?p1 ?z1 .
...	
?y ?pn ?zn .
    } 

EOF

echo  << EOF > 18-prp-npa1.rq
    PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>
    PREFIX rdf: <http://www.w3.org/1999/02/22-rdf-syntax-ns#>
    PREFIX owl: <http://www.w3.org/2002/07/owl#>

    INSERT { 
    GRAPH <http://industrialinference.com/inferred/> {
FALSE
    } } WHERE {
?x owl:sourceIndividual ?i1 .
?x owl:assertionProperty ?p .
?x owl:targetIndividual ?i2 .
?i1 ?p ?i2 .
    } 

EOF

echo  << EOF > 19-prp-npa2.rq
    PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>
    PREFIX rdf: <http://www.w3.org/1999/02/22-rdf-syntax-ns#>
    PREFIX owl: <http://www.w3.org/2002/07/owl#>

    INSERT { 
    GRAPH <http://industrialinference.com/inferred/> {
FALSE
    } } WHERE {
?x owl:sourceIndividual ?i .
?x owl:assertionProperty ?p .
?x owl:targetValue ?lt .
?i ?p ?lt .
    } 

EOF
