# Variables in Rules

I'm a little unclear about how to manage the sets of bindings that arise from a
BGP, and how to represent the variables vs concrete data in the BGP.

- [!] Assume that all parts of the BGP are triples made up of variables. Some
of the variables are pre-bound - the original concrete values in the BGP. Some
are not - the original variables in the BGP.

i.e. if the BGP looked like this:

```turtle
?a rdf:type ?t .
?t rdfs:label "harriot" .
```

we could perform a variable transformation to this:

```plain
(v1, ?a) (v2, rdf:type) (v3, ?t) .
(v3, ?t) (v4, rdfs:label) (v5, "harriot") .
```

giving a table of variables to unify, with or without bindings:

| v1  | v2       | v3  | v4         | v5        |
| --- | -------- | --- | ---------- | --------- |
|     | rdf:type |     | rdfs:label | "harriot" |

Which can be managed as a set of triples

| s   | p   | o   |
| --- | --- | --- |
| v1  | v2  | v3  |
| v3  | v4  | v5  |

say 