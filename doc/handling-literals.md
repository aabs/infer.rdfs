# Handling Literals

Right now, the triple store is geared towards storing and compressing Uris.
There needs to be a mechanism to signify that the data in the Object slot is a
reference to a literal value and should be saught in some other store.

## IDEA 1

Use a negative number to indicate the data is somewhere else. The entry could
be in a table of literals. The entry in the alternate store could be a
placeholder that contains a whole bunch of metadata such as:

- data type (XSD URI)
- location of the data (in memory/disk)
- number of copies in use
- location within a table for just this specific data type

## IDEA 2

Always use Idea 1, and just have URI as one of the data types supported. i.e.
All Object place entries are treated as literals, where URIs are just another
sort of literal that get to share the regular UriRegistry