<?php

	include 'baglanti.php';
	
	
	$tc_no = $_POST['tc_no'];
	$sifre = $_POST['sifre'];
	
	$sql = "SELECT * FROM hastabilgileri WHERE tc_no = ? AND sifre = ?";
	$params = array($tc_no, $sifre);
	$stmt = sqlsrv_query($conn, $sql, $params);
	if ($stmt === false) {
		die("bağlantı başarısız: " . sqlsrv_errors());
	}

	if (sqlsrv_has_rows($stmt)) {
	session_start();
	$_SESSION['tc_no'] = $tc_no;
	header("Location: /proje/index.html");
	exit();
	}	 
	else {
		if (empty($tc_no) || empty($sifre)) {
        echo '<script>alert("Lütfen tüm alanları doldurunuz!"); window.location.href = "/proje/login.html";</script>';
    }
		else
		echo '<script>alert("geçersiz tc no yada şifre"); window.location.href = "/proje/login.html";</script>';
	}
	
	sqlsrv_close($conn);
?>