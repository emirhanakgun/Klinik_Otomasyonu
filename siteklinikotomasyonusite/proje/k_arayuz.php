<?php

	include 'php/baglanti.php';
	

	session_start();
	
	
	

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
	
	$sql = "SELECT * FROM hastabilgileri WHERE tc_no='$tc_no'";
	$stmt = sqlsrv_query($conn, $sql);
	
	if ($stmt == false) {
    die(print_r(sqlsrv_errors(), true));
	}
	
	$row = sqlsrv_fetch_array($stmt, SQLSRV_FETCH_ASSOC);
	
	$adi = $row['adi'];
	$soyadi = $row['soyadi'];
	$cinsiyet = $row['cinsiyet'];
	$medeni_hal = $row['medeni_hal'];
	$dogum_tarihi = $row['dogum_tarihi'];
	
	
	
	sqlsrv_free_stmt($stmt);
	sqlsrv_close($conn);
?>



<!DOCTYPE html>
<html>
<head>


<link rel="stylesheet" href="css/k_arayuz.css">
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

<a href="k_arayuz.php" class="linkler" id="k_arayuz" style="background-color:#C0EB56; color:white;  float:right; ">kullanıcı</a>


</div>


<div class="orta" style="background-image: url('img/k_arayuz.png'); background-size: cover;">



<div id="kutu">


	<form method="post" action="" style="margin-top:50px;">
	
	<h1>tc_no: &nbsp; <?php echo $tc_no; ?> </h1>
	<br>
	<h1>adi:   &nbsp;&nbsp;&nbsp;&nbsp; <?php echo $adi; ?> </h1>
	<br>
	<h1>soyadi:  &nbsp; <?php echo $soyadi; ?> </h1>
	<br>
	<h1>cinsiyet: &nbsp; <?php echo $cinsiyet; ?> </h1>
	<br>
	<h1>medeni hal: &nbsp; <?php echo $medeni_hal; ?> </h1>
	<br>
	<h1>doğum tarihi: &nbsp; <?php echo $dogum_tarihi; ?> </h1> 
	
	
	
	<a href="k_arayuz2.php" id="guncelle" style="width:195px; height:60px; margin:50px 0px 0px 180px; float:left;">Güncelle</a>
    <input type="submit" name="logout" id="logout" value="Log Out" style="width:200px; height:60px; margin:8px 0px 0px 180px; float:left;">
    </form>

</div>











</div>









<div class="footer">


</div>











</body>
</html>