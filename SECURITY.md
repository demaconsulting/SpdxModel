# Security Policy

## Supported Versions

We release patches for security vulnerabilities in the following versions:

| Version | Supported          |
| ------- | ------------------ |
| Latest  | :white_check_mark: |
| < Latest| :x:                |

We recommend always using the latest version of SpdxModel to ensure you have the most recent security updates.

## Reporting a Vulnerability

The SpdxModel team takes security bugs seriously. We appreciate your efforts to responsibly disclose your findings.

### How to Report a Security Vulnerability

If you discover a security vulnerability, please follow these steps:

1. **Do not** open a public GitHub issue
2. Report the vulnerability using the
   [Security tab][security-tab] of this GitHub repository
3. Include the following information in your report:
   - Description of the vulnerability
   - Steps to reproduce the issue
   - Potential impact of the vulnerability
   - Any suggested fixes (if available)
   - Your contact information for follow-up

### What to Expect

When you report a security vulnerability, you can expect:

- **Acknowledgment**: We will acknowledge receipt of your report within 48 hours
- **Updates**: We will keep you informed of our progress in addressing the vulnerability
- **Fix Timeline**: We aim to address critical security issues within 7 days
- **Credit**: If you wish, we will credit you in the security advisory

### Security Update Process

1. **Assessment**: We will investigate and assess the severity of the reported vulnerability
2. **Fix Development**: We will develop and test a fix
3. **Release**: We will release a new version with the security fix
4. **Disclosure**: We will publish a security advisory detailing the vulnerability and the fix
5. **Notification**: We will notify users of the security update through our release notes

## Security Best Practices

When using SpdxModel, we recommend the following security best practices:

### Input Validation

- Always validate SPDX documents before processing
- Be cautious when processing SPDX documents from untrusted sources
- Implement appropriate error handling for malformed documents

### Dependency Management

- Keep SpdxModel and its dependencies up to date
- Regularly check for security updates
- Use tools like `dotnet list package --vulnerable` to check for vulnerable dependencies

### Code Scanning

- Use static analysis tools to detect potential security issues
- Enable all compiler warnings and treat them as errors
- Consider using tools like SonarCloud for continuous security monitoring

## Known Security Considerations

### JSON Deserialization

SpdxModel uses `System.Text.Json` for deserializing SPDX documents. While `System.Text.Json` is generally secure,
be aware of:

- **Large Documents**: Extremely large SPDX documents may cause memory issues
- **Deeply Nested Structures**: Deeply nested JSON structures may cause stack overflow
- **Malformed Input**: Always validate input before processing

### Regular Expressions

The library uses regular expressions for validation. To prevent ReDoS (Regular Expression Denial of Service) attacks:

- Regex patterns have timeout limits configured
- Avoid processing untrusted input without validation

## Security Tools Used

This project uses the following security tools:

- **SonarCloud**: For continuous code quality and security analysis
- **Dependabot**: For automated dependency updates
- **CodeQL**: For semantic code analysis
- **Microsoft.CodeAnalysis.NetAnalyzers**: For .NET-specific security analysis

## Responsible Disclosure

We believe in responsible disclosure of security vulnerabilities. We ask that you:

- Give us a reasonable amount of time to fix the issue before making it public
- Do not exploit the vulnerability beyond what is necessary to demonstrate it
- Do not access, modify, or delete data that is not yours

## Security Hall of Fame

We would like to thank the following individuals for responsibly disclosing security vulnerabilities:

*No vulnerabilities have been reported yet.*

## Contact

For security-related inquiries, please use the project's GitHub issue tracker (for non-sensitive issues) or the
[Security tab][security-tab] (for sensitive security issues).

## Additional Resources

- [OWASP Top 10][owasp-top-10]
- [.NET Security Guidelines][dotnet-security]
- [GitHub Security Best Practices][github-security]

Thank you for helping keep SpdxModel and its users safe!

[security-tab]: https://github.com/demaconsulting/SpdxModel/security/advisories/new
[owasp-top-10]: https://owasp.org/www-project-top-ten/
[dotnet-security]: https://docs.microsoft.com/en-us/dotnet/standard/security/
[github-security]: https://docs.github.com/en/code-security
