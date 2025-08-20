terraform {
  required_providers {
    azurerm = {
        source = "hashicorp/azurerm"
        version = "4.39.0"
    }
  }
  
  backend "azurerm" {
    resource_group_name = "capybarapetapp-rg"
    storage_account_name = "capybarapetappiacfiles"
    container_name = "terraform"
    key = "terraform.state"
  }
}

provider "azurerm" {
  features {}
  resource_provider_registrations = "none"
  subscription_id = var.subscription_id
}