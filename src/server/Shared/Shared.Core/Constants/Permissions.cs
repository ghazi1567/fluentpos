﻿// --------------------------------------------------------------------------------------------------
// <copyright file="Permissions.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System.ComponentModel;

namespace FluentPOS.Shared.Core.Constants
{
    public static class Permissions
    {
        [DisplayName("Users")]
        [Description("Users Permissions")]
        public static class Users
        {
            public const string View = "Permissions.Users.View";
            public const string Add = "Permissions.Users.Register";
            public const string Update = "Permissions.Users.Update";
            public const string Remove = "Permissions.Users.Remove";
        }

        [DisplayName("Users Extended Attributes")]
        [Description("Users Extended Attributes Permissions")]
        public static class UsersExtendedAttributes
        {
            public const string View = "Permissions.Users.ExtendedAttributes.View";
            public const string ViewAll = "Permissions.Users.ExtendedAttributes.ViewAll";
            public const string Add = "Permissions.Users.ExtendedAttributes.Register";
            public const string Update = "Permissions.Users.ExtendedAttributes.Update";
            public const string Remove = "Permissions.Users.ExtendedAttributes.Remove";
        }

        [DisplayName("Roles")]
        [Description("Roles Permissions")]
        public static class Roles
        {
            public const string View = "Permissions.Roles.View";
            public const string Create = "Permissions.Roles.Register";
            public const string Edit = "Permissions.Roles.Update";
            public const string Delete = "Permissions.Roles.Delete";
        }

        [DisplayName("Roles Extended Attributes")]
        [Description("Roles Extended Attributes Permissions")]
        public static class RolesExtendedAttributes
        {
            public const string View = "Permissions.Roles.ExtendedAttributes.View";
            public const string ViewAll = "Permissions.Roles.ExtendedAttributes.ViewAll";
            public const string Add = "Permissions.Roles.ExtendedAttributes.Register";
            public const string Update = "Permissions.Roles.ExtendedAttributes.Update";
            public const string Remove = "Permissions.Roles.ExtendedAttributes.Remove";
        }

        [DisplayName("Role Claims")]
        [Description("Role Claims Permissions")]
        public static class RoleClaims
        {
            public const string View = "Permissions.RoleClaims.View";
            public const string Create = "Permissions.RoleClaims.Register";
            public const string Edit = "Permissions.RoleClaims.Update";
            public const string Delete = "Permissions.RoleClaims.Delete";
        }

        [DisplayName("Brands")]
        [Description("Brands Permissions")]
        public static class Brands
        {
            public const string View = "Permissions.Brands.View";
            public const string ViewAll = "Permissions.Brands.ViewAll";
            public const string Register = "Permissions.Brands.Register";
            public const string Update = "Permissions.Brands.Update";
            public const string Remove = "Permissions.Brands.Remove";
        }

        [DisplayName("Brands Extended Attributes")]
        [Description("Brands Extended Attributes Permissions")]
        public static class BrandsExtendedAttributes
        {
            public const string View = "Permissions.Brands.ExtendedAttributes.View";
            public const string ViewAll = "Permissions.Brands.ExtendedAttributes.ViewAll";
            public const string Add = "Permissions.Brands.ExtendedAttributes.Register";
            public const string Update = "Permissions.Brands.ExtendedAttributes.Update";
            public const string Remove = "Permissions.Brands.ExtendedAttributes.Remove";
        }

        [DisplayName("Customers")]
        [Description("Customers Permissions")]
        public static class Customers
        {
            public const string View = "Permissions.Customers.View";
            public const string ViewAll = "Permissions.Customers.ViewAll";
            public const string Register = "Permissions.Customers.Register";
            public const string Update = "Permissions.Customers.Update";
            public const string Remove = "Permissions.Customers.Remove";
        }

        [DisplayName("Customers Extended Attributes")]
        [Description("Customers Extended Attributes Permissions")]
        public static class CustomersExtendedAttributes
        {
            public const string View = "Permissions.Customers.ExtendedAttributes.View";
            public const string ViewAll = "Permissions.Customers.ExtendedAttributes.ViewAll";
            public const string Add = "Permissions.Customers.ExtendedAttributes.Register";
            public const string Update = "Permissions.Customers.ExtendedAttributes.Update";
            public const string Remove = "Permissions.Customers.ExtendedAttributes.Remove";
        }

        [DisplayName("Categories")]
        [Description("Categories Permissions")]
        public static class Categories
        {
            public const string View = "Permissions.Categories.View";
            public const string ViewAll = "Permissions.Categories.ViewAll";
            public const string Register = "Permissions.Categories.Register";
            public const string Update = "Permissions.Categories.Update";
            public const string Remove = "Permissions.Categories.Remove";
        }

        [DisplayName("Categories Extended Attributes")]
        [Description("Categories Extended Attributes Permissions")]
        public static class CategoriesExtendedAttributes
        {
            public const string View = "Permissions.Categories.ExtendedAttributes.View";
            public const string ViewAll = "Permissions.Categories.ExtendedAttributes.ViewAll";
            public const string Add = "Permissions.Categories.ExtendedAttributes.Register";
            public const string Update = "Permissions.Categories.ExtendedAttributes.Update";
            public const string Remove = "Permissions.Categories.ExtendedAttributes.Remove";
        }

        [DisplayName("Products")]
        [Description("Products Permissions")]
        public static class Products
        {
            public const string View = "Permissions.Products.View";
            public const string ViewAll = "Permissions.Products.ViewAll";
            public const string Register = "Permissions.Products.Register";
            public const string Update = "Permissions.Products.Update";
            public const string Remove = "Permissions.Products.Remove";
            public const string Import = "Permissions.Products.Import";
        }

        [DisplayName("Products Extended Attributes")]
        [Description("Products Extended Attributes Permissions")]
        public static class ProductsExtendedAttributes
        {
            public const string View = "Permissions.Products.ExtendedAttributes.View";
            public const string ViewAll = "Permissions.Products.ExtendedAttributes.ViewAll";
            public const string Add = "Permissions.Products.ExtendedAttributes.Register";
            public const string Update = "Permissions.Products.ExtendedAttributes.Update";
            public const string Remove = "Permissions.Products.ExtendedAttributes.Remove";
        }

        [DisplayName("Carts")]
        [Description("Carts Permissions")]
        public static class Carts
        {
            public const string View = "Permissions.Carts.View";
            public const string ViewAll = "Permissions.Carts.ViewAll";
            public const string Create = "Permissions.Carts.Register";
            public const string Remove = "Permissions.Carts.Remove";
        }

        [DisplayName("Carts Extended Attributes")]
        [Description("Carts Extended Attributes Permissions")]
        public static class CartsExtendedAttributes
        {
            public const string View = "Permissions.Carts.ExtendedAttributes.View";
            public const string ViewAll = "Permissions.Carts.ExtendedAttributes.ViewAll";
            public const string Add = "Permissions.Carts.ExtendedAttributes.Register";
            public const string Update = "Permissions.Carts.ExtendedAttributes.Update";
            public const string Remove = "Permissions.Carts.ExtendedAttributes.Remove";
        }

        [DisplayName("Cart Items")]
        [Description("Cart Items Permissions")]
        public static class CartItems
        {
            public const string View = "Permissions.CartItems.View";
            public const string ViewAll = "Permissions.CartItems.ViewAll";
            public const string Add = "Permissions.CartItems.Register";
            public const string Update = "Permissions.CartItems.Update";
            public const string Remove = "Permissions.CartItems.Remove";
        }

        [DisplayName("Cart Items Extended Attributes")]
        [Description("Cart Items Extended Attributes Permissions")]
        public static class CartItemsExtendedAttributes
        {
            public const string View = "Permissions.CartItems.ExtendedAttributes.View";
            public const string ViewAll = "Permissions.CartItems.ExtendedAttributes.ViewAll";
            public const string Add = "Permissions.CartItems.ExtendedAttributes.Register";
            public const string Update = "Permissions.CartItems.ExtendedAttributes.Update";
            public const string Remove = "Permissions.CartItems.ExtendedAttributes.Remove";
        }

        [DisplayName("Event Logs")]
        [Description("Event Logs Permissions")]
        public static class EventLogs
        {
            public const string ViewAll = "Permissions.EventLogs.ViewAll";
            public const string Create = "Permissions.EventLogs.Register";
        }

        [DisplayName("Sales")]
        [Description("Sales Permissions")]
        public static class Sales
        {
            public const string View = "Permissions.Sales.View";
            public const string ViewAll = "Permissions.Sales.ViewAll";
            public const string Register = "Permissions.Sales.Register";
            public const string Update = "Permissions.Sales.Update";
            public const string Remove = "Permissions.Sales.Remove";
        }

        [DisplayName("StockIn")]
        [Description("Stock In Permissions")]
        public static class StockIn
        {
            public const string View = "Permissions.StockIn.View";
            public const string ViewAll = "Permissions.StockIn.ViewAll";
            public const string Register = "Permissions.StockIn.Register";
            public const string Update = "Permissions.StockIn.Update";
            public const string Remove = "Permissions.StockIn.Remove";
            public const string Approval = "Permissions.StockIn.Approval";

        }

        [DisplayName("StockOut")]
        [Description("Stock Out Permissions")]
        public static class StockOut
        {
            public const string View = "Permissions.StockOut.View";
            public const string ViewAll = "Permissions.StockOut.ViewAll";
            public const string Register = "Permissions.StockOut.Register";
            public const string Update = "Permissions.StockOut.Update";
            public const string Remove = "Permissions.StockOut.Remove";
        }

        [DisplayName("PurchaseOrder")]
        [Description("Purchase Order Permissions")]
        public static class PurchaseOrder
        {
            public const string View = "Permissions.PurchaseOrder.View";
            public const string ViewAll = "Permissions.PurchaseOrder.ViewAll";
            public const string Register = "Permissions.PurchaseOrder.Register";
            public const string Update = "Permissions.PurchaseOrder.Update";
            public const string Remove = "Permissions.PurchaseOrder.Remove";
        }

        [DisplayName("Branchs")]
        [Description("Branchs Permissions")]
        public static class Branchs
        {
            public const string View = "Permissions.Branchs.View";
            public const string ViewAll = "Permissions.Branchs.ViewAll";
            public const string Register = "Permissions.Branchs.Register";
            public const string Update = "Permissions.Branchs.Update";
            public const string Remove = "Permissions.Branchs.Remove";
        }

        [DisplayName("Organizations")]
        [Description("Organizations Permissions")]
        public static class Organizations
        {
            public const string View = "Permissions.Organizations.View";
            public const string ViewAll = "Permissions.Organizations.ViewAll";
            public const string Register = "Permissions.Organizations.Register";
            public const string Update = "Permissions.Organizations.Update";
            public const string Remove = "Permissions.Organizations.Remove";
        }

        [DisplayName("Departments")]
        [Description("Departments Permissions")]
        public static class Departments
        {
            public const string View = "Permissions.Departments.View";
            public const string ViewAll = "Permissions.Departments.ViewAll";
            public const string Register = "Permissions.Departments.Register";
            public const string Update = "Permissions.Departments.Update";
            public const string Remove = "Permissions.Departments.Remove";
        }

        [DisplayName("Designations")]
        [Description("Designations Permissions")]
        public static class Designations
        {
            public const string View = "Permissions.Designations.View";
            public const string ViewAll = "Permissions.Designations.ViewAll";
            public const string Register = "Permissions.Designations.Register";
            public const string Update = "Permissions.Designations.Update";
            public const string Remove = "Permissions.Designations.Remove";
        }

        [DisplayName("Policy")]
        [Description("Policy Permissions")]
        public static class Policy
        {
            public const string View = "Permissions.Policy.View";
            public const string ViewAll = "Permissions.Policy.ViewAll";
            public const string Register = "Permissions.Policy.Register";
            public const string Update = "Permissions.Policy.Update";
            public const string Remove = "Permissions.Policy.Remove";
        }

        [DisplayName("Employees")]
        [Description("Employees Permissions")]
        public static class Employees
        {
            public const string View = "Permissions.Employees.View";
            public const string ViewAll = "Permissions.Employees.ViewAll";
            public const string Register = "Permissions.Employees.Register";
            public const string Update = "Permissions.Employees.Update";
            public const string Remove = "Permissions.Employees.Remove";
        }

        [DisplayName("AttendanceRequests")]
        [Description("Attendance Requests Permissions")]
        public static class AttendanceRequests
        {
            public const string View = "Permissions.AttendanceRequests.View";
            public const string ViewAll = "Permissions.AttendanceRequests.ViewAll";
            public const string Register = "Permissions.AttendanceRequests.Register";
            public const string Update = "Permissions.AttendanceRequests.Update";
            public const string Remove = "Permissions.AttendanceRequests.Remove";
            public const string MyQueue = "Permissions.AttendanceRequests.MyQueue";
        }

        [DisplayName("OvertimeRequests")]
        [Description("Overtime Requests Permissions")]
        public static class OvertimeRequests
        {
            public const string View = "Permissions.OvertimeRequests.View";
            public const string ViewAll = "Permissions.OvertimeRequests.ViewAll";
            public const string Register = "Permissions.OvertimeRequests.Register";
            public const string Update = "Permissions.OvertimeRequests.Update";
            public const string Remove = "Permissions.OvertimeRequests.Remove";
            public const string MyQueue = "Permissions.OvertimeRequests.MyQueue";
        }

    }
}