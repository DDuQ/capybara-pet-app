resource "azurerm_log_analytics_workspace" "law" {
  location            = azurerm_resource_group.capybarapetapprg.location
  name                = "${var.appname}-law-${var.env_id}"
  resource_group_name = azurerm_resource_group.capybarapetapprg.name
  sku                 = "PerGB2018"
  retention_in_days   = 30

  tags = {
    environment = var.env_id
    src = var.src_key
  }
}
