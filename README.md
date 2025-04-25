# Overview

The objective of this repo is to enable querying of Dataverse environment using SQL using MCP. You can use popular AI tools such as GitHub Copilot or Claude Desktop to query Dataverse, provided you add this MCP Server in the configuration.

# Installation

[![Install with NPM in VS Code](https://img.shields.io/badge/VS_Code-dotnet-0098FF?style=flat-square&logo=visualstudiocode&logoColor=white)](vscode:mcp/install?%7B%0A%20%20%20%20%22name%22%3A%20%22dataverse-mcp-dotnet-tool%22%2C%0A%20%20%20%20%22command%22%3A%20%22mcp-dataverse%22%2C%0A%20%20%20%20%22env%22%3A%20%7B%0A%20%20%20%20%20%20%20%20%22DATAVERSE_ENVIRONMENT_URL%22%3A%20%22https%3A%2F%2Fxyz.crm.dynamics.com%22%0A%20%20%20%20%7D%0A%7D%0A)&nbsp;&nbsp;
[![Install with Docker in VS Code](https://img.shields.io/badge/VS_Code-Docker-0098FF?style=flat-square&logo=visualstudiocode&logoColor=white)](vscode:mcp/install?%7B%0A%20%20%20%20%22name%22%3A%20%22dataverse-mcp-docker%22%2C%0A%20%20%20%20%22command%22%3A%20%22docker%22%2C%0A%20%20%20%20%22args%22%3A%20%5B%0A%20%20%20%20%20%20%20%20%22run%22%2C%0A%20%20%20%20%20%20%20%20%22--env-file%22%2C%0A%20%20%20%20%20%20%20%20%22%24%7BworkspaceFolder%7D%2F.env%22%2C%0A%20%20%20%20%20%20%20%20%22-i%22%2C%0A%20%20%20%20%20%20%20%20%22--rm%22%2C%0A%20%20%20%20%20%20%20%20%22rajyraman%2Fmcp-stdio-dataverse%22%0A%20%20%20%20%5D%0A%7D)
