﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketGlobal.CustomClass
{
	public class claNumber
	{

		/// 입력된 문자열이 숫자인지 아닌지 판단 합니다.
		public bool IsNumeric(string value)
		{
			if ((null == value)
				|| (true == string.IsNullOrEmpty(value))
				|| ("" == value))
			{
				//널이다
				return false;
			}

			int nIndex = 0;

			foreach (char cData in value)
			{
				if (false == Char.IsNumber(cData))
				{
					//인덱스가 0일때 '-'는 부호가 될수 있으므로 숫자로 판단한다.
					if ((0 == nIndex)
						&& ('-' != cData))
					{
						return false;
					}
				}

				++nIndex;
			}
			return true;
		}

	}
}
