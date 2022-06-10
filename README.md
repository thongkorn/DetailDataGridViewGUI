# Detail DataGridView with GUI - Visual Basic .NET (2010)
# การออกแบบหน้าจอการขายสินค้า GUI แบบกราฟิค

โค้ดชุดนี้ถือว่าอยู่ในขั้นตอนของการออกแบบ จึงไม่มีการติดต่อเข้ากับระบบฐานข้อมูลใดๆ แต่อาศัยการใช้ข้อมูลสมมุติ หรือจำลอง ผ่านทาง DataTable ซึ่งจะมีการแบ่งกลุ่มหรือประเภทสินค้าเอาไว้ให้ 4 กลุ่ม และสามารถแสดงผลรายการสินค้าทั้งหมดออกมาได้ ... ในการสร้าง Control ในลักษณะไดนามิคแบบนี้ (คือไม่รู้จำนวนรายการสินค้า หรือจำนวนกลุ่มที่แน่นอน) ก็ไม่ได้ยากเย็นมากนัก เริ่มจากการหาจำนวนกลุ่มสินค้าออกมาก่อน (ตัวอย่างมี 4 กลุ่ม + กลุ่มรวมทั้งหมด จะได้ 5 กลุ่ม) จากนั้นก็สร้าง TabPage และ Panel Control (เพื่อตีกรอบ Button Control ไม่ให้ล้น) มารองรับในแต่ละกลุ่ม เมื่อได้ Panel Control มาแล้ว ก็นำเอาจำนวนสินค้าที่อยู่ในแต่ละกลุ่มมาทำการแสดงผล โดยที่มีการคำนวณหาระยะของ Button อยู่ 2 ค่า คือ ... หาค่าตำแหน่ง Left จะมาจากการหารเอาเศษ (MOD) และหาตำแหน่งบน (Top) ด้วยการหารตัดเศษ (\)
