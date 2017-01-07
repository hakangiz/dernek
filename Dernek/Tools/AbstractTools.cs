using Dernek.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dernek.Tools
{
	abstract class AbstractTools
	{

        public static double MemberPayTotalForActivity(activity activity)
        {
            //%30 oyuncu % 70 dernek parametrelerden çekilecek
            double dancerPayment = (activity.price * 0.30) / activity.ApplicationUsers.Count();
            var remaining = dancerPayment % 5;
            if (remaining > 0) {
                dancerPayment = dancerPayment + (5 - remaining);
            }
            return dancerPayment;
        }
	}
}