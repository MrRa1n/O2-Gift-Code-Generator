# O2 Gifts - JBL Flip 4 Voucher Generator
This is a program I have created that attempts to generate working vouchers for O2 Gifts' JBL Flip 4 speaker offer.

It is a fairly simple application that uses Selenium WebDriver to automate the process of loading the browser and inputting the voucher and IMEI into the text box objects.

The first step of the program is to load the O2 Gifts website using the WebDriver (specifically Google Chrome). Once the website has been loaded, a voucher and IMEI is generated and sent to the text box objects automatically. I realised generating both at the same time would take years to find a match, so I implemented the following solution.

## Methodology - IMEI
I already had a working voucher at hand, so I was only left to find valid IMEI numbers. I looked at a number of different IMEIs for S8 handsets and analyzed the patterns in them.

The IMEIs are made up of 15 digits and there were multiple occurrences of the same 12 digits `("358792080521")`. This left me only having to find the remaining 3 digits. Easy.

I created a GenerateIMEI() function that took two arguments; IMEI prefix and length. The prefix was the first 12 digits and the length was how many we wanted to add to the end of the prefix, which was 3. Allowing that to run while using the working voucher yielded about 10 IMEIs in 2-3 minutes.

Now all I have to do is generate the voucher. Not so easy.

## Methodology - Voucher
The voucher itself consists of the first 5 characters being letters (A-Z) and the remaining 5 characters being numbers (0-9). Using a simple bit of maths, this means we have 26 possibilties for each position, so it is `26(^5)`. The second part can be any number from 0-9, giving us 10 possibilities for each position, meaning it is `10(^5)`.

So, our equation works out as...
>`26(^5) * 10(^5)` = `11,881,376 * 100,000` = `1,188,137,600,000`

There are *1,188,137,600,000* possible combinations... Yikes.

I decided to try it anyway because, you know, I'm a bit bored... 

I created the function to randomly generate each voucher and had assigned to the `generatedVoucher` variable. A string array is then created that takes every line from a text file called `invalid_vouchers.txt` and stores it. The `generatedVoucher` variable is compared to the contents of the array to see if the voucher has been previously generated (avoid reusing same voucher) and if it is, a new voucher is generated. 

The voucher and IMEI are entered into the text box objects using ChromeDriver and checked if they have worked. This is done by simply checking if the URL property of the `chromeDriver` class has changed. If the voucher works, it is written to a new text file called `valid_vouchers.txt` and the program restarts its main method.

If the voucher is invalid, it is writted to the `invalid_vouchers.txt` file and the applications continues its iteration.

## Conclusion
So at the current rate, if I generate 20,000 vouchers every day, it should only take me around `162758.57534` years to find all the matches :). I remain optimistic. To speed things up I will be looking at maybe hosting the text files on a centralized server so they can be accessed by more than one client machine at any given time, hopefully speeding up the process.

**Just to be clear, I'm not trying to dupe O2 out of anything. I'm just a bit bored and wanted to do a refresher on my programming skillz. Hey, maybe it'll be educational for all you other folk as well.**

**Peace out homies.**
