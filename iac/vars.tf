variable "env_id" {
    type = string
    description = "The environment id"
    default = "dev"
}

variable "appname" {
    type = string
    description = "The name of the app"
    default = "capybarapetapp"
}

variable "src_key" {
    type = string
    description = "The infrastructure source"
    default = "terraform"
}
variable "subscription_id" {
    type = string
    description = "The Azure subscription id"
    default = "dd28d209-7ba1-4ba1-ab36-0cd05e2e3308"
}

variable "sql_pass" {
    type = string
    description = "The SQL Server password"
}