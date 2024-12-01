﻿using IdentityWithJwtTestProject.DataAccessLayer.Attributes;
using IdentityWithJwtTestProject.DataAccessLayer.Datas;
using IdentityWithJwtTestProject.DataAccessLayer.Enums;
using IdentityWithJwtTestProject.DataAccessLayer.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IdentityWithJwtTestProject.DataAccessLayer.Services.Concrete
{
    public class ApplicationService : IApplicationService
    {
        public List<Menu> GetAuthorizeDefinitionEndpoints(Type type)
        {
            var assembly = Assembly.GetAssembly(type);
            var controllers = assembly.GetTypes().Where(t => t.IsAssignableTo(typeof(ControllerBase)));

            var menus = new List<Menu>();

            if (controllers == null || !controllers.Any())
                throw new NullReferenceException("Controllers cannot be null or empty");

            foreach (var controller in controllers)
            {
                var actions = controller.GetMethods().Where(m => m.IsDefined(typeof(AuthorizeDefinitionAttribute)));

                if (!actions.Any())
                    continue;

                foreach (var action in actions)
                {
                    var authorizeDefinitionAttribute = action.GetCustomAttributes(true)
                        .OfType<AuthorizeDefinitionAttribute>()
                        .FirstOrDefault();

                    if (authorizeDefinitionAttribute == null)
                        continue;

                    var menu = menus.FirstOrDefault(m => m.Name == authorizeDefinitionAttribute.MenuName)
                            ?? new Menu
                            {
                                Name = authorizeDefinitionAttribute.MenuName,
                                Actions = new List<Datas.Action>()
                            };

                    if (!menus.Contains(menu))
                        menus.Add(menu);

                    var actionType = Enum.TryParse<ActionType>(authorizeDefinitionAttribute.ActionType.ToString(), out var parsedActionType)
                        ? parsedActionType
                        : throw new ArgumentException("Invalid ActionType value");

                    var httpAttribute = action.GetCustomAttributes(true)
                        .OfType<HttpMethodAttribute>()
                        .FirstOrDefault();

                    var httpType = httpAttribute?.HttpMethods.FirstOrDefault() ?? HttpMethods.Get;

                    var _action = new Datas.Action
                    {
                        ActionType = actionType,
                        Definition = authorizeDefinitionAttribute.Definition,
                        HttpType = httpType,
                        Code = $"{httpType}.{actionType}.{authorizeDefinitionAttribute.Definition.Replace(" ", "")}"
                    };

                    menu.Actions.Add(_action);
                }
            }

            return menus;
        }

    }
}