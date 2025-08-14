resource "azurerm_resource_group" "capybarapetapprg" {
  location = "UK South"
  name = "capybara-pet-app-rg"
  
  tags = {
    environment = var.env_id
    src = var.src_key
  }
}