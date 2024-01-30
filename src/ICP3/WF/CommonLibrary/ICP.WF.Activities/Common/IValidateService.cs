using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICP.WF.Activities 
{
	public interface IValidateService
	{
        bool Validate(List<string> errors);
	}
}
