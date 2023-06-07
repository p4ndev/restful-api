# ETAG Meaning
Entity Tag

# In a nutshell
Hashing the entire content and uses together with requests based on add/update operations.

# Goal
Decrease the needs of write data operations (execution).

# Where should it be used
On next requests that data might change it sends the etag together (put, post or patch).

# How it works
Before any write the operations with providers, check whether content has changed:

- changed: writes, and then recompile a new etag
- otherwise: dismiss the execution

# Negative though(s)
There will be a cost for encrypt/decrypt this content.