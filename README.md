# Overview

The objective of this repo is to enable querying of Dataverse environment using SQL. You can use popular AI tools such as GitHub Copilot or Claude Desktop to query Dataverse, provided you add this MCP Server in the configuration.

A big thank you to [Mark Carrington](https://www.linkedin.com/in/MarkMpn/) for creating [Sql4Cds](https://github.com/MarkMpn/Sql4Cds), without whom this project would not exist. 🙏

# Prerequisites 🔍

1. VSCode March (minimum March 2025 version) or Claude Desktop
2. Azure CLI installed and authenticated on your local machine, if you are not using the DevContainer option. While interactive browser authentication is possible using Azure Identity framework, using Azure CLI to manage the authentication is little easier.
3. [Docker](https://docker.com), if you want to run Dev Containers or the MCP Server inside DevContainer.
4. dotnet 9.0 SDK if you want to install the dotnet tool.

## Model Context Protocol (MCP) 📋

This is how Anthropic, the creators of MCP specification, defines [Model Context Protocol](https://modelcontextprotocol.io/introduction)

> MCP is an open protocol that standardizes how applications provide context to LLMs. Think of MCP like a USB-C port for AI applications. Just as USB-C provides a standardized way to connect your devices to various peripherals and accessories, MCP provides a standardized way to connect AI models to different data sources and tools.

# Installation 🚀

[![Install as dotnet](https://img.shields.io/badge/VS_Code-Add_MCP_dotnet_tool_config-blue?logo=data:image/png;base64,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)](https://vscode.dev/redirect?url=vscode:mcp/install?%7B%0A%20%20%20%20%22name%22%3A%20%22dataverse-mcp-dotnet-tool%22%2C%0A%20%20%20%20%22type%22%3A%20%22stdio%22%2C%0A%20%20%20%20%22command%22%3A%20%22mcp-dataverse%22%2C%0A%20%20%20%20%22env%22%3A%20%7B%0A%20%20%20%20%20%20%20%20%22DATAVERSE_ENVIRONMENT_URL%22%3A%20%22https%3A%2F%2Fxyz.crm.dynamics.com%22%0A%20%20%20%20%7D%0A%7D)

If the button abobe does not work, paste the below URL to the address bar.
```
vscode:mcp/install?%7B%0A%20%20%20%20%22name%22%3A%20%22dataverse-mcp-dotnet-tool%22%2C%0A%20%20%20%20%22type%22%3A%20%22stdio%22%2C%0A%20%20%20%20%22command%22%3A%20%22mcp-dataverse%22%2C%0A%20%20%20%20%22env%22%3A%20%7B%0A%20%20%20%20%20%20%20%20%22DATAVERSE_ENVIRONMENT_URL%22%3A%20%22https%3A%2F%2Fxyz.crm.dynamics.com%22%0A%20%20%20%20%7D%0A%7D
```

[![Install with Docker in VS Code](https://img.shields.io/badge/VS_Code-Add_MCP_Docker_config-0098FF?logo=data:image/png;base64,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)](https://vscode.dev/redirect?url=vscode:mcp/install?%7B%0A%20%20%20%20%22name%22%3A%20%22dataverse-mcp-docker%22%2C%0A%20%20%20%20%22type%22%3A%20%22stdio%22%2C%0A%20%20%20%20%22command%22%3A%20%22docker%22%2C%0A%20%20%20%20%22args%22%3A%20%5B%0A%20%20%20%20%20%20%20%20%22run%22%2C%0A%20%20%20%20%20%20%20%20%22--env-file%22%2C%0A%20%20%20%20%20%20%20%20%22%24%7BworkspaceFolder%7D%2F.env%22%2C%0A%20%20%20%20%20%20%20%20%22-i%22%2C%0A%20%20%20%20%20%20%20%20%22--rm%22%2C%0A%20%20%20%20%20%20%20%20%22rajyraman%2Fmcp-stdio-dataverse%22%0A%20%20%20%20%5D%0A%7D)

If the button abobe does not work, paste the below URL to the address bar.
```
vscode:mcp/install?%7B%0A%20%20%20%20%22name%22%3A%20%22dataverse-mcp-docker%22%2C%0A%20%20%20%20%22type%22%3A%20%22stdio%22%2C%0A%20%20%20%20%22command%22%3A%20%22docker%22%2C%0A%20%20%20%20%22args%22%3A%20%5B%0A%20%20%20%20%20%20%20%20%22run%22%2C%0A%20%20%20%20%20%20%20%20%22--env-file%22%2C%0A%20%20%20%20%20%20%20%20%22%24%7BworkspaceFolder%7D%2F.env%22%2C%0A%20%20%20%20%20%20%20%20%22-i%22%2C%0A%20%20%20%20%20%20%20%20%22--rm%22%2C%0A%20%20%20%20%20%20%20%20%22rajyraman%2Fmcp-stdio-dataverse%22%0A%20%20%20%20%5D%0A%7D
```

You can click the buttons above to get the MCP Server config settings. You have two options to use this MCP Server:

1. Docker Container
2. dotnet tool

Below is the recommendation based on the where you plan to use this MCP

| In                | Client                         | Recommendation                                                       |
| ----------------- | ------------------------------ | -------------------------------------------------------------------- |
| Windows           | GitHub Copilot                 | dotnet tool. Use Docker/Podman as fallback, if you have auth issues. |
| Windows           | Claude Desktop                 | dotnet tool                                                          |
| MacOS             | Claude Desktop, GitHub Copilot | dotnet tool                                                          |
| Linux             | Claude Desktop, GitHub Copilot | dotnet tool                                                          |
| GitHub Codespaces | GitHub Copilot                 | dotnet tool                                                          |

Since this MCP server is built with [MCP C# SDK](https://github.com/modelcontextprotocol/csharp-sdk), it is distributed via nuget as a dotnet tool. dotnet 9.0 SDK can be installed in all major OSes.

You can install the Dataverse MCP Server as a global dotnet tool using the command below.

```
dotnet tool install -g Mcp.Dataverse.Stdio
```

# Configuration

Below is a sample .env file that you can use if you choose to run the MCP Server inside a container. Create the .env on the workspace folder (same level as the README.md).

```
AZURE_CLIENT_ID=aba9829f-6288-44d7-9168-53eca9a1f4a5
AZURE_CLIENT_SECRET=abcd
AZURE_TENANT_ID=2caa17e6-884b-473b-80c5-c05d8859a2fa
DATAVERSE_ENVIRONMENT_URL=https://abc.crm6.dynamics.com
DOCKER_CONTAINER=true
```

# Configuration for GitHub Codespaces

When you create you Codespace make sure you select the [devcontainer.json](.devcontainer/nuget-tool/devcontainer.json) inside the [nuget-tool](.devcontainer/nuget-tool/) folder. You should also change the [dataverse-nosecrets.env](.devcontainer/nuget-tool/dataverse-nosecrets.env) so that the DATAVERSE_ENVIRONMENT_URL environment variable is pointing to the right environment URL.

![codespace-step-1](./images/codespace-create-1.jpg)

![codespace-step-2](./images/codespace-create-2.jpg)

# Prompts

If you are using GitHub Copilot, use the prompt [dataverse.main.prompt.md](.github/prompts/dataverse.main.prompt.md) with your Agent requests.

![Select Prompt](/images/select-prompt.jpg)

On Claude Desktop, you can clicking the "Attach from MCP" button.

![Select Prompt](/images/select-prompt-claude.jpg)

# Examples

## Example 1 - Get Unmanaged Solutions 📦

![Unmanaged Solutions](./images/unmanaged-solutions-claude.jpg)

## Example 2 - Show Solutions by Publisher 🏢

![Solutions by Publisher](./images/show-solutions.jpg)

## Example 3 - Solutions Imported last week 📅

![Solutions imported last week](./images/solutions-imported-last-week.jpg)

## Example 4 - Plugins registered on contact and account 🔌

![Plugin Steps](./images/plugin-steps.jpg)

## Example 5 - Running SQL directly with #ExecuteSQL tool

![Direct SQL](./images/direct-sql.jpg)
