function ValidarFecha(selDia,selMes,selAno)
{	var Ano = selAno; 
	var Mes = selMes;
	var Dia = selDia;
		
	if(Dia > '' && Mes>'' && Ano >'')
	{	if (isNaN(Ano) || Ano.length<4 || parseFloat(Ano)<1900)
		{	return false;	}
		if (isNaN(Mes) || parseFloat(Mes)<1 || parseFloat(Mes)>12)
		{	return false;	}
		if (isNaN(Dia) || parseInt(Dia,10)<1 || parseInt(Dia,10)>31)
		{	return false;	}
		if (Mes==4 || Mes==6 || Mes==9 || Mes==11 || Mes==2) 
		{	if (Ano%4==0)
			{	if (Mes==2 && Dia > 29 || Dia>30) 
				{	return false;	}
			}
			else
			{	if (Mes==2 && Dia > 28 || Dia>30) 
				{	return false;	}
			}
		}
	}
	else 
	{	if(Dia > '' || Mes>'' || Ano >'')
		{	return false	}
		else
		{	return false	}
	}  
}

function validarSoloNumeros()
{	if (event.keyCode <45  || event.keyCode >57 )
	{ 
	event.returnValue = false;
	}
}

function protegercodigo()
{	if (event.button==2||event.button==3) 
	{	alert('Minera Barrick Misquichilca S.A.\n\n Dpto. Contabilidad y Finanzas');
	}
}
