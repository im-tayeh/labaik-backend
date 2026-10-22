# .NET 11 migration checklist

Search the repository for `NET11:` before starting. Every hit is listed here.

- [ ] Directory.Build.props: TargetFramework net10.0 -> net11.0
- [ ] global.json: sdk 10.0.100 -> 11.0.100
- [ ] Dockerfile: sdk:10.0 -> sdk:11.0, aspnet:10.0-noble-chiseled-extra -> aspnet:11.0-noble-chiseled-extra (verify tags on MCR)
- [ ] Packages that follow the .NET major (Microsoft.AspNetCore.*, Microsoft.Extensions.*, Microsoft.EntityFrameworkCore.*): 10.0.x -> 11.0.x
