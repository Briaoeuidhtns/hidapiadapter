using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace HidApiAdapter
{
    public class HidApi
    {
        /// <summary>
        /// Initialize the HIDAPI library.
        /// This function initializes the HIDAPI library.
        /// Calling it is not strictly necessary, as it will be called automatically by hid_enumerate() and any of the hid_open_*() functions if it is needed.
        /// This function should be called at the beginning of execution however, if there is a chance of HIDAPI handles being opened by different threads simultaneously.
        /// </summary>
        /// <returns>This function returns 0 on success and -1 on error</returns>
        public static int hid_init()
        {
            return HidApiWin.hid_init();
        }

        /// <summary>
        /// Finalize the HIDAPI library.
        /// This function frees all of the static data associated with HIDAPI. 
        /// It should be called at the end of execution to avoid memory leaks.
        /// </summary>
        /// <returns>This function returns 0 on success and -1 on error.</returns>
        public static int hid_exit()
        {
            return HidApiWin.hid_exit();
        }

        /// <summary>
        /// Enumerate the HID Devices.
        /// This function returns a linked list of all the HID devices attached to the system which match vendor_id and product_id. 
        /// If vendor_id is set to 0 then any vendor matches. If product_id is set to 0 then any product matches. 
        /// If vendor_id and product_id are both set to 0, then all HID devices will be returned. 
        /// </summary>
        /// <param name="vendor_id">The Vendor ID (VID) of the types of device to open.</param>
        /// <param name="product_id">The Product ID (PID) of the types of device to open.</param>
        /// <returns>
        /// This function returns a pointer to a linked list of type struct hid_device, 
        /// containing information about the HID devices attached to the system, or NULL in the case of failure. 
        /// Free this linked list by calling hid_free_enumeration().
        /// </returns>
        public static IntPtr hid_enumerate(ushort vendor_id, ushort product_id)
        {
            return HidApiWin.hid_enumerate(vendor_id, product_id);
        }

        /// <summary>
        /// Free an enumeration Linked List.
        /// This function frees a linked list created by hid_enumerate().
        /// </summary>
        /// <param name="devs">Pointer to a list of struct_device returned from hid_enumerate().</param>
        public static void hid_free_enumeration(IntPtr devs)
        {
            HidApiWin.hid_free_enumeration(devs);
        }

        /// <summary>
        /// Open a HID device using a Vendor ID (VID), Product ID (PID) and optionally a serial number.
        /// If serial_number is NULL, the first device with the specified VID and PID is opened.
        /// </summary>
        /// <param name="vendor_id">The Vendor ID (VID) of the device to open</param>
        /// <param name="product_id">The Product ID (PID) of the device to open</param>
        /// <param name="serial_number">The Serial Number of the device to open (Optionally NULL)</param>
        /// <returns>This function returns a pointer to a hid_device object on success or NULL on failure</returns>
        public static IntPtr hid_open(ushort vendor_id, ushort product_id, string serial_number)
        {
            return HidApiWin.hid_open(vendor_id, product_id, serial_number);
        }

        /// <summary>
        /// Open a HID device by its path name.
        /// The path name be determined by calling hid_enumerate(), or a platform-specific path name can be used(eg: /dev/hidraw0 on Linux)
        /// </summary>
        /// <param name="path">The path name of the device to open</param>
        /// <returns>This function returns a pointer to a hid_device object on success or NULL on failure</returns>
        public static IntPtr hid_open_path(IntPtr path)
        {
            return HidApiWin.hid_open_path(path);

        }

        /// <summary>
        /// Write an Output report to a HID device.
        /// The first byte of data[] must contain the Report ID.For devices which only support a single report, this must be set to 0x0. 
        /// The remaining bytes contain the report data.Since the Report ID is mandatory, 
        /// calls to hid_write() will always contain one more byte than the report contains. 
        /// For example, if a hid report is 16 bytes long, 17 bytes must be passed to hid_write(), the Report ID (or 0x0, for devices with a single report), 
        /// followed by the report data(16 bytes). In this example, the length passed in would be 17.
        /// hid_write() will send the data on the first OUT endpoint, if one exists. If it does not, it will send the data through the Control Endpoint (Endpoint 0)
        /// </summary>
        /// <param name="device">A device handle returned from hid_open()</param>
        /// <param name="data">The data to send, including the report number as the first byte</param>
        /// <param name="length">The length in bytes of the data to send</param>
        /// <returns>This function returns the actual number of bytes written and -1 on error</returns>
        public static int hid_write(IntPtr device, byte[] data, uint length)
        {
            return HidApiWin.hid_write(device, data, length);
        }

        /// <summary>
        /// Read an Input report from a HID device with timeout. 
        /// Input reports are returned to the host through the INTERRUPT IN endpoint. 
        /// The first byte will contain the Report number if the device uses numbered reports.
        /// </summary>
        /// <param name="device">A device handle returned from hid_open().</param>
        /// <param name="data">A buffer to put the read data into</param>
        /// <param name="length">The number of bytes to read. For devices with multiple reports, make sure to read an extra byte for the report number</param>
        /// <param name="milliseconds">Timeout in milliseconds or -1 for blocking wait</param>
        /// <returns>
        /// This function returns the actual number of bytes read and -1 on error. 
        /// If no packet was available to be read within the timeout period, this function returns 0
        /// </returns>
        public static int hid_read_timeout(IntPtr device, byte[] data, uint length, int milliseconds)
        {
            return HidApiWin.hid_read_timeout(device, data, length, milliseconds);
        }

        /// <summary>
        /// Read an Input report from a HID device.
        /// Input reports are returned to the host through the INTERRUPT IN endpoint. 
        /// The first byte will contain the Report number if the device uses numbered reports.
        /// </summary>
        /// <param name="device">A device handle returned from hid_open()</param>
        /// <param name="data">A buffer to put the read data into</param>
        /// <param name="length">The number of bytes to read. For devices with multiple reports, make sure to read an extra byte for the report number</param>
        /// <returns>
        /// This function returns the actual number of bytes read and -1 on error. 
        /// If no packet was available to be read and the handle is in non-blocking mode, this function returns 0
        /// </returns>
        public static int hid_read(IntPtr device, byte[] data, uint length)
        {
            return HidApiWin.hid_read(device, data, length);
        }

        /// <summary>
        /// Set the device handle to be non-blocking. 
        /// In non-blocking mode calls to hid_read() will return immediately with a value of 0 if there is no data to be read. 
        /// In blocking mode, hid_read() will wait(block) until there is data to read before returning.
        /// Nonblocking can be turned on and off at any time 
        /// </summary>
        /// <param name="device">A device handle returned from hid_open()</param>
        /// <param name="nonblock">Enable(1) or not(0) the nonblocking reads</param>
        /// <returns>This function returns 0 on success and -1 on error</returns>
        public int hid_set_nonblocking(IntPtr device, int nonblock)
        {
            return HidApiWin.hid_set_nonblocking(device, nonblock);
        }

        /// <summary>
        /// Send a Feature report to the device.
        /// Feature reports are sent over the Control endpoint as a Set_Report transfer. 
        /// The first byte of data[] must contain the Report ID.For devices which only support a single report, this must be set to 0x0. 
        /// The remaining bytes contain the report data.Since the Report ID is mandatory, 
        /// calls to hid_send_feature_report() will always contain one more byte than the report contains. 
        /// For example, if a hid report is 16 bytes long, 17 bytes must be passed to hid_send_feature_report(): the Report ID (or 0x0, for devices which do not use numbered reports), 
        /// followed by the report data(16 bytes). In this example, the length passed in would be 17.
        /// </summary>
        /// <param name="device">A device handle returned from hid_open()</param>
        /// <param name="data">The data to send, including the report number as the first byte</param>
        /// <param name="length">The length in bytes of the data to send, including the report number</param>
        /// <returns>This function returns the actual number of bytes written and -1 on error</returns>
        public static int hid_send_feature_report(IntPtr device, byte[] data, uint length)
        {
            return HidApiWin.hid_send_feature_report(device, data, length);
        }

        /// <summary>
        /// Get a feature report from a HID device.
        /// Set the first byte of data[] to the Report ID of the report to be read.
        /// Make sure to allow space for this extra byte in data[]. 
        /// Upon return, the first byte will still contain the Report ID, and the report data will start in data[1].
        /// </summary>
        /// <param name="device">A device handle returned from hid_open()</param>
        /// <param name="data">
        /// A buffer to put the read data into, including the Report ID. 
        /// Set the first byte of data[] to the Report ID of the report to be read, or set it to zero if your device does not use numbered reports
        /// </param>
        /// <param name="length">The number of bytes to read, including an extra byte for the report ID. The buffer can be longer than the actual report</param>
        /// <returns>This function returns the number of bytes read plus one for the report ID (which is still in the first byte), or -1 on error</returns>
        public static int hid_get_feature_report(IntPtr device, byte[] data, uint length)
        {
            return HidApiWin.hid_get_feature_report(device, data, length);
        }

        /// <summary>
        /// Close a HID device.
        /// </summary>
        /// <param name="device">A device handle returned from hid_open()</param>
        public static void hid_close(IntPtr device)
        {
                HidApiWin.hid_close(device);
        }

        /// <summary>
        /// Get The Manufacturer String from a HID device.
        /// </summary>
        /// <param name="device">A device handle returned from hid_open()</param>
        /// <param name="str">A wide string buffer to put the data into.</param>
        /// <param name="length">The length of the buffer in multiples of wchar_t</param>
        /// <returns>This function returns 0 on success and -1 on error</returns>
        public static int hid_get_manufacturer_string(IntPtr device, StringBuilder str, uint length)
        {
            return HidApiWin.hid_get_manufacturer_string(device, str, length);
        }

        /// <summary>
        /// Get The Product String from a HID device.
        /// </summary>
        /// <param name="device">A device handle returned from hid_open()</param>
        /// <param name="str">A wide string buffer to put the data into</param>
        /// <param name="length">The length of the buffer in multiples of wchar_t</param>
        /// <returns>This function returns 0 on success and -1 on error</returns>
        public static int hid_get_product_string(IntPtr device, StringBuilder str, uint length)
        {
            return HidApiWin.hid_get_product_string(device, str, length);
        }

        /// <summary>
        /// Get The Product String from a HID device
        /// </summary>
        /// <param name="device">A device handle returned from hid_open()</param>
        /// <param name="serial">A wide string buffer to put the data into</param>
        /// <param name="maxlen">The length of the buffer in multiples of wchar_t</param>
        /// <returns>This function returns 0 on success and -1 on error</returns>
        public static int hid_get_serial_number_string(IntPtr device, StringBuilder serial, uint maxlen)
        {
            return HidApiWin.hid_get_serial_number_string(device, serial, maxlen);
        }

        /// <summary>
        /// Get a string from a HID device, based on its string index.
        /// </summary>
        /// <param name="device">A device handle returned from hid_open()</param>
        /// <param name="string_index">The index of the string to get</param>
        /// <param name="str">A wide string buffer to put the data into</param>
        /// <param name="maxlen">The length of the buffer in multiples of wchar_t</param>
        /// <returns>This function returns 0 on success and -1 on error.</returns>
        public static int hid_get_indexed_string(IntPtr device, int string_index, StringBuilder str, uint maxlen)
        {
            return HidApiWin.hid_get_indexed_string(device, string_index, str, maxlen);
        }

        /// <summary>
        /// Get a string describing the last error which occurred.
        /// </summary>
        /// <param name="device">A device handle returned from hid_open()</param>
        /// <returns>This function returns a string containing the last error which occurred or NULL if none has occurred.</returns>
        public static IntPtr hid_error(IntPtr device)
        {
                return HidApiWin.hid_error(device);
        }
    }
}