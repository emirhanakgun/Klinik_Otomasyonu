<?php


	include 'php/baglanti.php';
	

	session_start();
	
	
	
	
	$saatler = array(
	'10:00',
	'11:00',
	'12:00',
	'13:00',
	'14:00',
	'15:00',
	'16:00',
	'17:00',
	'18:00'
	);
	
	
	
	
	if (isset($_POST['logout'])) {
	session_destroy();


	header("Location: /proje/login.html");
	exit();
	}
	
	
	
	
	if (!isset($_SESSION['tc_no'])) {
	header("Location: /proje/login.html");
	exit();
	}
	
	
	$tc_no=$_SESSION['tc_no'];
	
	$sql5 = "SELECT * FROM randevu WHERE tc_no = ?";
	$params2 = array($tc_no);
	$stmt5 = sqlsrv_query($conn, $sql5, $params2);
	if ($stmt5 === false) {
	    die(print_r(sqlsrv_errors(), true));
	}
	
	
	
	
	
	
	//
	
	
	
	
	
	$sql = "SELECT * FROM hastabilgileri WHERE tc_no='$tc_no'";
	$stmt = sqlsrv_query($conn, $sql);
	
	if ($stmt == false) {
    die(print_r(sqlsrv_errors(), true));
	}
	
	$row = sqlsrv_fetch_array($stmt, SQLSRV_FETCH_ASSOC);
	
	
	
	//
	
	
	
	$sql2 = "SELECT brans_adi,brans_id FROM branslar";
	$stmt2 = sqlsrv_query($conn, $sql2);
	
	if($stmt2 === false) {
    die(print_r(sqlsrv_errors(), true));
	}
	

	$options = "";
	while($row2 = sqlsrv_fetch_array($stmt2, SQLSRV_FETCH_ASSOC)) {
    $options .= "<option value='".$row2['brans_id']."'  label='".$row2['brans_adi']."'>".$row2['brans_adi']."</option>";
	}
	
	
	
	//
	
	
	
	$sql3 = "SELECT doktor_tc_no,brans_id,doktor_adi,doktor_soyadi FROM doktorlar";
	$stmt3 = sqlsrv_query($conn, $sql3);
	
	if($stmt3 === false) {
    die(print_r(sqlsrv_errors(), true));
	}
	

	$options2 = array();
	while($row3 = sqlsrv_fetch_array($stmt3, SQLSRV_FETCH_ASSOC)) {
    if(!array_key_exists($row3['brans_id'], $options2)) {
        $options2[$row3['brans_id']] = array();
    } 
	$options2[$row3['brans_id']][]= $row3['doktor_tc_no'];
     
  
	}
	
	
	
	

	if (isset($_POST['brans'])) {
    $brans = $_POST['brans'];
	} else {
    $brans = '';
	}
	if (isset($_POST['time'])) {
    $time = $_POST['time'];
	} else {
    $time = '';
	}
	if (isset($_POST['doktor'])) {
    $doktor = $_POST['doktor'];
	} else {
    $doktor = '';
	}
	if (isset($_POST['randevu_tarihi'])) {
    $randevu_tarihi = $_POST['randevu_tarihi'];
	} else {
    $randevu_tarihi = '';
	}
	
	if(isset($_POST['gonder'])){
	
	if(empty($brans) || empty($doktor) || empty($randevu_tarihi)){ echo"<script>alert('Lütfen boş alanları doldurunuz.');</script>"; }
	
	else{
		
		
	$sql7 = "SELECT COUNT(*) AS randevu_say FROM randevu WHERE randevu_tarihi = ? AND randevu_saati = ? AND doktor_id = ?";
	$params7 = array($randevu_tarihi, $time, $doktor);
	$stmt7 = sqlsrv_query($conn, $sql7, $params7);
	if ($stmt7 === false) {
		die(print_r(sqlsrv_errors(), true)); 
	}
	$row7 = sqlsrv_fetch_array($stmt7, SQLSRV_FETCH_ASSOC);
	$randevu_say = $row7['randevu_say'];


	if ($randevu_say > 0) {
		echo "<script>alert('Üzgünüz, bu tarihte bu doktora randevu alamazsınız.');</script>";
	}
	
	else{
	
	
	
	if (isset($_POST['gonder'])) {
	$sql4 ="INSERT INTO randevu (tc_no, randevu_tarihi, randevu_saati, brans_id, doktor_id) VALUES (?, ?, ?, ?, ?)";
	$params = array($tc_no, $randevu_tarihi, $time, $brans, $doktor);
	$stmt4 = sqlsrv_query($conn, $sql4, $params);
	
	if (!$stmt4) {
    die( print_r( sqlsrv_errors(), true) );
				}
	
				}
	
	
		}
	
	
		}
	
	}
	
	
	
	
	
	if (isset($_POST['delete'])) {
	$randevu_id = $_POST['randevu_id'];


	$sql6 = "DELETE FROM randevu WHERE randevu_id = ?";
	$stmt6 = sqlsrv_prepare($conn, $sql6, array(&$randevu_id));

	if ($stmt6) {
		$result = sqlsrv_execute($stmt6);
		
		if ($result) {
		header("Location: randevu.php");
		exit();
		} else {
		echo "Error executing SQL query: " . sqlsrv_errors()[0]['message'];
		}
	} 
	else {
		echo "Error preparing SQL query: " . sqlsrv_errors()[0]['message'];
	}
	}
	
	
	
	
	sqlsrv_free_stmt($stmt3);
	sqlsrv_free_stmt($stmt2);
	sqlsrv_free_stmt($stmt);
	//sqlsrv_close($conn);
?>



<!DOCTYPE html>
<html>
<head>


<link rel="stylesheet" href="css/randevu.css">
<meta charset="UTF-8">


</head>
<body>
<div class="ust_serit"></div>
<div class="header">

<div class="logo">

  <img src="img/logo.jpg" style="width:500px; height:100px; float:left;">

</div>

<a href="index.html" class="linkler">Anasayfa</a>

<a href="index2.html" class="linkler">Hakkımızda</a>

<a href="index3.html" class="linkler">Galeri</a>

<a href="index4.html" class="linkler">İletişim</a>

<a href="login.html" class="linkler" id="giris_b" style="background-color:#FF0B5C; float:right; color:white;">Giriş</a>

<a href="k_arayuz.php" class="linkler" id="k_arayuz" style="background-color:#C0EB56; color:white; float:right; ">kullanıcı</a>


</div>


<div class="orta" style="background-image: url('img/randevu.jpeg'); background-size: cover;">


<div id="kutu">

<form id="randevu" method="POST">

	<h1>tc_no: <br> <?php echo $tc_no; ?></h1><br>
	
	<h1>Tarih Seçimi:</h1> <input type="date" name="randevu_tarihi" style="width:230px; height:40px; float:left; border-style: inset; margin-left:100px;"> </input> <br><br><br>
	
	<h1>Saat Seçimi:</h1>	<select name="time" id="time" style="width:230px; height:40px; float:left; border-style: inset; margin-left:100px;">
								<?php foreach ($saatler as $saat): ?>
								<option value="<?php echo $saat; ?>"><?php echo $saat; ?></option>
								<?php endforeach; ?>
							</select><br><br>
	
	
	<h1>Brans Seçimi:<br><h1> <select id="brans" name="brans" style="width:230px; height:40px; float:left; border-style: inset; margin-left:100px;">
								<option># Branş Seç #</option>
								<?php echo $options; ?>
								</select> <br>
	
	<h1>Doktor Seçimi:<br><h1> <select id="doktor" name="doktor" style="width:230px; height:40px; float:left; border-style: inset; margin-left:100px;">
									</select> <br><br>
	
									
	<input type="submit" id="gonder" name="gonder" style=" margin-top:30px; width:200px; height:40px; float:left; margin-left:120px;" onClick="window.location.reload();"></input>
	


</form>



<script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
<script>
        // Get the select elements
        var branchSelect = document.getElementById("brans");
        var doctorSelect = document.getElementById("doktor");

        // Create a mapping of branch IDs to doctor names
        var doctorsByBranch = <?php echo json_encode($options2); ?>;

        // Update the doctor select options when the branch select value changes
        branchSelect.addEventListener("change", function() {
            // Get the selected branch ID
            var selectedBranchId = branchSelect.value;
			
			

            // Clear the doctor select options
            doctorSelect.innerHTML = "";

            // Add the doctors for the selected branch to the doctor select options
            for(var i = 0; i < doctorsByBranch[selectedBranchId].length; i++) {
                var doctorName = doctorsByBranch[selectedBranchId][i];
                var option = document.createElement("option");
                option.value = doctorName;
                option.text = doctorName;
                doctorSelect.add(option);
            }
        });
    </script>                   





</div>








<div id="kutu2">




<h2 style="margin-left:42%;">Aktif randevularım</h2>
	<table style="margin-left:10px;">
		<thead>
			<tr>
				<th style="padding-left:50px;"><h2>Randevu ID</h2></th>
				<th style="padding-left:50px;"><h2>Randevu Tarihi</h2></th>
				<th style="padding-left:50px;"><h2>Brans ID</h2></th>
				<th style="padding-left:50px;"><h2>Doktor ID</h2></th>
				<th style="padding-left:50px;"><h2></h2></th>
			</tr>
		</thead>
		<tbody>
			<?php 
			
			while ($row4 = sqlsrv_fetch_array($stmt5, SQLSRV_FETCH_ASSOC)) {  ?>
			<tr>
				<td style="padding-left:80px;"><?php echo $row4["randevu_id"]?></td>
				<td style="padding-left:80px;"><?php echo $row4["randevu_tarihi"]," ",$row4["randevu_saati"]?></td>
				<td style="padding-left:80px;"><?php echo $row4["brans_id"]; ?></td>
				<td style="padding-left:80px;"><?php echo $row4["doktor_id"]; ?></td>
				<td style='padding-left:80px;'>
				<form method='post'>
					<input type='hidden' name='randevu_id' value='<?php echo $row4["randevu_id"] ?>'>
					<input type='submit' name='delete' value='İptal et' style="border-radius: 17px; color: white; background-color:#B060EB;">
				</form>
				</td>
			</tr>
			<?php }  ?>
		</tbody>
	</table>





</div>



</div>









<div class="footer">


</div>











</body>
</html>