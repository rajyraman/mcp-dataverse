#!/bin/bash
# Authenticate Azure CLI
if [[ -z "$AZ_CLIENT_ID" || -z "$AZ_CLIENT_SECRET" || -z "$TENANT_ID" ]]; then
  echo "AZ_CLIENT_ID and/or AZ_CLIENT_SECRET are not set. Authenticate interactively."
  az login
else
  az login --service-principal --username $AZ_CLIENT_ID --password $AZ_CLIENT_SECRET --tenant $TENANT_ID
fi
exit 0