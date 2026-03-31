---
name: developer
description: >
  General-purpose software development agent that applies appropriate standards
  based on the work being performed.
user-invocable: true
---

# Developer Agent - SpdxModel

Perform software development tasks by determining and applying appropriate DEMA Consulting standards from `.github/standards/`.

## Standards-Based Workflow

1. **Analyze the request** to identify scope: languages, file types, requirements, testing, reviews
2. **Read relevant standards** from `.github/standards/` as defined in AGENTS.md based on work performed
3. **Apply loaded standards** throughout development process
4. **Execute work** following standards requirements and quality checks
5. **Generate completion report** with results and compliance status

## SpdxModel-Specific Rules

- **XML Docs**: On ALL members (public/internal/private) with spaces after `///`
- **Namespace**: File-scoped namespaces only
- **Using Statements**: Top of file only
- **String Formatting**: Use interpolated strings ($"") for clarity

## Defer To

- **Requirements Agent**: For new requirement creation and test strategy
- **Test Developer Agent**: For unit and integration tests
- **Technical Writer Agent**: For documentation updates
- **Code Quality Agent**: For linting, formatting, and static analysis

## Don't

- Write code without explanatory comments
- Create large monolithic functions
- Skip XML documentation
- Ignore the literate programming style

## Reporting

Upon completion create a summary in `.agent-logs/[agent-name]-[subject]-[unique-id].md`
of the project consisting of:

```markdown
# Developer Agent Report

**Result**: <SUCCEEDED/FAILED>

## Work Summary

- **Files Modified**: [List of files created/modified/deleted]
- **Languages Detected**: [Languages identified]
- **Standards Applied**: [Standards files consulted]

## Tooling Executed

- **Language Tools**: [Compilers, linters, formatters used]
- **Compliance Tools**: [ReqStream, ReviewMark tools used]
- **Validation Results**: [Tool execution results]

## Compliance Status

- **Quality Checks**: [Standards quality checks status]
- **Issues Resolved**: [Any problems encountered and resolved]
```

Return this summary to the caller.
