on_load =()=>{
	
	document.getElementById('enquiry-form').style.display="none";
	document.getElementById('meet-employee').style.display="none";
	document.getElementById('meet-others').style.display="none";
   
	
}
	window.onload=on_load();

// Radio Button Click
document.getElementById('open-form-btn').onclick = function(){
	document.getElementById('table-listing').style.display="none";
	document.getElementById('enquiry-form').style.display="block";
   
}
document.getElementById('back-to-list').onclick = function(){
	document.getElementById('table-listing').style.display="block";    
	document.getElementById('enquiry-form').style.display="none";
}



function ChangeDropdowns(value){
    if(value=="student"){
        document.getElementById('meet-others').style.display='none';
		document.getElementById('meet-employee').style.display='none';
		document.getElementById('meet-student').style.display='block';
    }else if(value=="employee"){
		document.getElementById('meet-others').style.display='none';
		document.getElementById('meet-employee').style.display='block';
		document.getElementById('meet-student').style.display='none';
    }
	else if(value=="others"){
		document.getElementById('meet-others').style.display='block';
		document.getElementById('meet-employee').style.display='none';
		document.getElementById('meet-student').style.display='none';
    }
}