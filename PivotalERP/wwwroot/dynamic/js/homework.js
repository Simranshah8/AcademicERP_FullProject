
on_load =()=>{
	
	document.getElementById('homework-type-form').style.display="none";
	document.getElementById('add-homework-form').style.display="none";
	document.getElementById('details-form').style.display="none";

   
	
}
	window.onload=on_load();

// Radio Button Click
document.getElementById('add-homework-type-btn').onclick = function(){
// console.log('add button clicked');
	document.getElementById('homework-type-section').style.display="none";
	document.getElementById('homework-type-form').style.display="block";
	// document.getElementById('homework-list').style.display="block";
   
}

document.getElementById('back-homework-btn').onclick = function(){
	document.getElementById('homework-type-section').style.display="block";
    
	document.getElementById('homework-type-form').style.display="none";
}

// Add Homework
document.getElementById('add-homework-btn').onclick = function(){
	// console.log('add button clicked');
		document.getElementById('add-homework-section').style.display="none";
		document.getElementById('add-homework-form').style.display="block";
	   
	}
	
	document.getElementById('back-add-homework-btn').onclick = function(){
		document.getElementById('add-homework-form').style.display="none";
		
		document.getElementById('add-homework-section').style.display="block";
	}

	document.getElementById('not-submitted-tab').onclick = function(){
		// console.log('add button clicked');
		
			document.getElementById('not-submitted').style.display="show";
		   
		}
	// Details
	document.getElementById('show-details-btn').onclick = function(){
		// console.log('add button clicked');
			document.getElementById('homework-list-section').style.display="none";
			document.getElementById('details-form').style.display="block";
			// document.getElementById('header-tabs').style.display="none";
			// document.getElementById('submitted').style.display="block";
			document.getElementById('not-submitted').style.display="none";
		   
		}

		

		document.getElementById('back-homework-list-btn').onclick = function(){
			document.getElementById('details-form').style.display="none";
			
			document.getElementById('homework-list-section').style.display="block";
		}

	document.getElementById('not-submitted-tab').onclick = function(){
		// console.log('add button clicked');
			document.getElementById('not-submitted').style.display="block";
			// document.getElementById('submitted').style.display="block";
			// document.getElementById('submitted').style.display="block";
		   
		}
	document.getElementById('submitted-tab').onclick = function(){
		// console.log('add button clicked');
			document.getElementById('not-submitted').style.display="none";
			// document.getElementById('submitted').style.display="block";
			// document.getElementById('submitted').style.display="block";
		   
		}
