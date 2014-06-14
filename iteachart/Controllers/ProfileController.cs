using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Infrastructure.Models;

namespace iteachart.Controllers
{
    public class ProfileController : BaseSecureController
    {
        // GET: /Profile/
        public ActionResult Index()
        {
            var model = new UserProfileModel
            {
                UserName = "Andrey Karavaychik",
                Department = "D3",
                Position = "Software Engineer",
                Skills = new List<UserProfileSkill>()
            };
            model.Skills.Add(
                    new UserProfileSkill
                    {
                        SkillName = ".NET",
                        SubSkills = new List<UserProfileSubSkill>
                        {
                            new UserProfileSubSkill
                            {
                                SkillName = "WinForms",
                                IsApproved = true
                            },
                            new UserProfileSubSkill
                            {
                                SkillName = "ASP.NET",
                                IsApproved = true
                            },
                            new UserProfileSubSkill
                            {
                                SkillName = "WCF",
                                IsApproved = false
                            },
                            new UserProfileSubSkill
                            {
                                SkillName = "WPF",
                                IsApproved = true
                            },
                    }
                    }); 
            model.Skills.Add(
                    new UserProfileSkill
                    {
                        SkillName = "CSS",
                        SubSkills = new List<UserProfileSubSkill>
                        {
                            new UserProfileSubSkill
                            {
                                SkillName = "CSS3",
                                IsApproved = true
                            },
                            new UserProfileSubSkill
                            {
                                SkillName = "CSS Best practice",
                                IsApproved = true
                            }
                    }
                    });

            model.Skills.Add(
                    new UserProfileSkill
                    {
                        SkillName = "Javascript",
                        SubSkills = new List<UserProfileSubSkill>
                        {
                            new UserProfileSubSkill
                            {
                                SkillName = "Knockout",
                                IsApproved = true
                            },
                            new UserProfileSubSkill
                            {
                                SkillName = "AngularJS",
                                IsApproved = true
                            }
                    }
                    });
            return View(model);
        }
    }
}