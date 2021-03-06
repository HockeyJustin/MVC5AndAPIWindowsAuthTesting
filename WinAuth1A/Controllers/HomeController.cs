﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WinAuth1A.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}


		/// <summary>
		/// http://stackoverflow.com/questions/17871816/login-as-another-user-mvc-4-windows-authentication
		/// </summary>
		/// <returns></returns>
		public ActionResult LogOut()
		{
			HttpCookie cookie = Request.Cookies["TSWA-Last-User"];

			if(String.IsNullOrWhiteSpace(User.Identity.Name))
			{
				return RedirectToAction("Index");
			}


			if (User.Identity.IsAuthenticated == false || cookie == null || StringComparer.OrdinalIgnoreCase.Equals(User.Identity.Name, cookie.Value))
			{
				string name = string.Empty;

				if (Request.IsAuthenticated)
				{
					name = User.Identity.Name;
				}

				cookie = new HttpCookie("TSWA-Last-User", name);
				Response.Cookies.Set(cookie);

				Response.AppendHeader("Connection", "close");
				Response.StatusCode = 401; // 403 Forbidden 401; // Unauthorized;
				Response.Clear();
				//should probably do a redirect here to the unauthorized/failed login page
				//if you know how to do this, please tap it on the comments below
				Response.Write("Unauthorized. Reload the page to try again...");
				Response.End();

				return RedirectToAction("Index");
			}

			cookie = new HttpCookie("TSWA-Last-User", string.Empty)
			{
				Expires = DateTime.Now.AddYears(-5)
			};

			Response.Cookies.Set(cookie);

			return RedirectToAction("Index");

		}
	}
}