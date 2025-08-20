resource "azurerm_resource_group" "capybarapetapprg" {
  location = "UK South"
  name = "capybarapetapp-rg"
  
  tags = {
    environment = var.env_id
    src = var.src_key
    review = "demo"
  }
}