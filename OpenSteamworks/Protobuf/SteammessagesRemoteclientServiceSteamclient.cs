// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: steammessages_remoteclient_service.steamclient.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021, 8981
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
/// <summary>Holder for reflection information generated from steammessages_remoteclient_service.steamclient.proto</summary>
public static partial class SteammessagesRemoteclientServiceSteamclientReflection {

  #region Descriptor
  /// <summary>File descriptor for steammessages_remoteclient_service.steamclient.proto</summary>
  public static pbr::FileDescriptor Descriptor {
    get { return descriptor; }
  }
  private static pbr::FileDescriptor descriptor;

  static SteammessagesRemoteclientServiceSteamclientReflection() {
    byte[] descriptorData = global::System.Convert.FromBase64String(
        string.Concat(
          "CjRzdGVhbW1lc3NhZ2VzX3JlbW90ZWNsaWVudF9zZXJ2aWNlLnN0ZWFtY2xp",
          "ZW50LnByb3RvGhhzdGVhbW1lc3NhZ2VzX2Jhc2UucHJvdG8aLHN0ZWFtbWVz",
          "c2FnZXNfdW5pZmllZF9iYXNlLnN0ZWFtY2xpZW50LnByb3RvGjFzdGVhbW1l",
          "c3NhZ2VzX3JlbW90ZWNsaWVudF9zZXJ2aWNlX21lc3NhZ2VzLnByb3RvMuwP",
          "CgxSZW1vdGVDbGllbnQSiAEKDkdldFBhaXJpbmdJbmZvEiUuQ1JlbW90ZUNs",
          "aWVudF9HZXRQYWlyaW5nSW5mb19SZXF1ZXN0GiYuQ1JlbW90ZUNsaWVudF9H",
          "ZXRQYWlyaW5nSW5mb19SZXNwb25zZSIngrUYI0dldCBwYWlyaW5nIGluZm8g",
          "Zm9yIGFuIGVudGVyZWQgUElOEn4KDE5vdGlmeU9ubGluZRIiLkNSZW1vdGVD",
          "bGllbnRfT25saW5lX05vdGlmaWNhdGlvbhoLLk5vUmVzcG9uc2UiPYK1GDlM",
          "ZXQgdGhlIHNlcnZpY2Uga25vdyB3ZSdyZSBhdmFpbGFibGUgZm9yIHN0YXR1",
          "cyBsaXN0ZW5lcnMSbgoRTm90aWZ5UmVwbHlQYWNrZXQSJy5DUmVtb3RlQ2xp",
          "ZW50X1JlcGx5UGFja2V0X05vdGlmaWNhdGlvbhoLLk5vUmVzcG9uc2UiI4K1",
          "GB9TZW5kIGEgcmVwbHkgdG8gYSByZW1vdGUgY2xpZW50Ep8BChJBbGxvY2F0",
          "ZVRVUk5TZXJ2ZXISKS5DUmVtb3RlQ2xpZW50X0FsbG9jYXRlVFVSTlNlcnZl",
          "cl9SZXF1ZXN0GiouQ1JlbW90ZUNsaWVudF9BbGxvY2F0ZVRVUk5TZXJ2ZXJf",
          "UmVzcG9uc2UiMoK1GC5BbGxvY2F0ZSBhIFRVUk4gc2VydmVyIGZvciBhIHN0",
          "cmVhbWluZyBzZXNzaW9uEqcBChNBbGxvY2F0ZVJlbGF5U2VydmVyEiouQ1Jl",
          "bW90ZUNsaWVudF9BbGxvY2F0ZVJlbGF5U2VydmVyX1JlcXVlc3QaKy5DUmVt",
          "b3RlQ2xpZW50X0FsbG9jYXRlUmVsYXlTZXJ2ZXJfUmVzcG9uc2UiN4K1GDNB",
          "bGxvY2F0ZSBhIFVEUCByZWxheSBzZXJ2ZXIgZm9yIGEgc3RyZWFtaW5nIHNl",
          "c3Npb24SfQoLQWxsb2NhdGVTRFISIi5DUmVtb3RlQ2xpZW50X0FsbG9jYXRl",
          "U0RSX1JlcXVlc3QaIy5DUmVtb3RlQ2xpZW50X0FsbG9jYXRlU0RSX1Jlc3Bv",
          "bnNlIiWCtRghQWxsb2NhdGUgU0RSIHJlc291cmNlcyBmb3IgYW4gYXBwEoMB",
          "ChhTZW5kU3RlYW1Ccm9hZGNhc3RQYWNrZXQSKi5DUmVtb3RlQ2xpZW50X1N0",
          "ZWFtQnJvYWRjYXN0X05vdGlmaWNhdGlvbhoLLk5vUmVzcG9uc2UiLoK1GCpC",
          "cm9hZGNhc3QgYSBwYWNrZXQgdG8gcmVtb3RlIFN0ZWFtIGNsaWVudHMSewoW",
          "U2VuZFN0ZWFtVG9TdGVhbVBhY2tldBIoLkNSZW1vdGVDbGllbnRfU3RlYW1U",
          "b1N0ZWFtX05vdGlmaWNhdGlvbhoLLk5vUmVzcG9uc2UiKoK1GCZTZW5kIGEg",
          "cGFja2V0IHRvIGEgcmVtb3RlIFN0ZWFtIGNsaWVudBKoAQocU2VuZFJlbW90",
          "ZVBsYXlTZXNzaW9uU3RhcnRlZBIjLkNSZW1vdGVQbGF5X1Nlc3Npb25TdGFy",
          "dGVkX1JlcXVlc3QaJC5DUmVtb3RlUGxheV9TZXNzaW9uU3RhcnRlZF9SZXNw",
          "b25zZSI9grUYOUxldCB0aGUgc2VydmVyIGtub3cgdGhhdCB3ZSBzdGFydGVk",
          "IGEgUmVtb3RlIFBsYXkgc2Vzc2lvbhKUAQocU2VuZFJlbW90ZVBsYXlTZXNz",
          "aW9uU3RvcHBlZBIoLkNSZW1vdGVQbGF5X1Nlc3Npb25TdG9wcGVkX05vdGlm",
          "aWNhdGlvbhoLLk5vUmVzcG9uc2UiPYK1GDlMZXQgdGhlIHNlcnZlciBrbm93",
          "IHRoYXQgd2Ugc3RvcHBlZCBhIFJlbW90ZSBQbGF5IHNlc3Npb24SiAEKHFNl",
          "bmRSZW1vdGVQbGF5VG9nZXRoZXJQYWNrZXQSIS5DUmVtb3RlUGxheVRvZ2V0",
          "aGVyX05vdGlmaWNhdGlvbhoLLk5vUmVzcG9uc2UiOIK1GDRTZW5kIGEgUmVt",
          "b3RlIFBsYXkgVG9nZXRoZXIgcGFja2V0IHRvIGEgU3RlYW0gY2xpZW50EskB",
          "CiJDcmVhdGVSZW1vdGVQbGF5VG9nZXRoZXJJbnZpdGF0aW9uEjkuQ1JlbW90",
          "ZUNsaWVudF9DcmVhdGVSZW1vdGVQbGF5VG9nZXRoZXJJbnZpdGF0aW9uX1Jl",
          "cXVlc3QaOi5DUmVtb3RlQ2xpZW50X0NyZWF0ZVJlbW90ZVBsYXlUb2dldGhl",
          "ckludml0YXRpb25fUmVzcG9uc2UiLIK1GChDcmVhdGUgYSBSZW1vdGUgUGxh",
          "eSBUb2dldGhlciBpbnZpdGF0aW9uEskBCiJEZWxldGVSZW1vdGVQbGF5VG9n",
          "ZXRoZXJJbnZpdGF0aW9uEjkuQ1JlbW90ZUNsaWVudF9EZWxldGVSZW1vdGVQ",
          "bGF5VG9nZXRoZXJJbnZpdGF0aW9uX1JlcXVlc3QaOi5DUmVtb3RlQ2xpZW50",
          "X0RlbGV0ZVJlbW90ZVBsYXlUb2dldGhlckludml0YXRpb25fUmVzcG9uc2Ui",
          "LIK1GChEZWxldGUgYSBSZW1vdGUgUGxheSBUb2dldGhlciBpbnZpdGF0aW9u",
          "Gi6CtRgqTWV0aG9kcyBmb3IgU3RlYW0gcmVtb3RlIGNsaWVudCBvcGVyYXRp",
          "b25zMpQHChdSZW1vdGVDbGllbnRTdGVhbUNsaWVudBKQAQoaTm90aWZ5UmVn",
          "aXN0ZXJTdGF0dXNVcGRhdGUSMC5DUmVtb3RlQ2xpZW50X1JlZ2lzdGVyU3Rh",
          "dHVzVXBkYXRlX05vdGlmaWNhdGlvbhoLLk5vUmVzcG9uc2UiM4K1GC9SZWdp",
          "c3RlciBmb3Igc3RhdHVzIHVwZGF0ZXMgd2l0aCBhIFN0ZWFtIGNsaWVudBKW",
          "AQocTm90aWZ5VW5yZWdpc3RlclN0YXR1c1VwZGF0ZRIyLkNSZW1vdGVDbGll",
          "bnRfVW5yZWdpc3RlclN0YXR1c1VwZGF0ZV9Ob3RpZmljYXRpb24aCy5Ob1Jl",
          "c3BvbnNlIjWCtRgxVW5yZWdpc3RlciBmb3Igc3RhdHVzIHVwZGF0ZXMgd2l0",
          "aCBhIFN0ZWFtIGNsaWVudBJwChJOb3RpZnlSZW1vdGVQYWNrZXQSKC5DUmVt",
          "b3RlQ2xpZW50X1JlbW90ZVBhY2tldF9Ob3RpZmljYXRpb24aCy5Ob1Jlc3Bv",
          "bnNlIiOCtRgfU2VuZCBhIHBhY2tldCB0byBhIFN0ZWFtIGNsaWVudBKFAQoa",
          "Tm90aWZ5U3RlYW1Ccm9hZGNhc3RQYWNrZXQSKi5DUmVtb3RlQ2xpZW50X1N0",
          "ZWFtQnJvYWRjYXN0X05vdGlmaWNhdGlvbhoLLk5vUmVzcG9uc2UiLoK1GCpC",
          "cm9hZGNhc3QgYSBwYWNrZXQgdG8gcmVtb3RlIFN0ZWFtIGNsaWVudHMSkQEK",
          "GE5vdGlmeVN0ZWFtVG9TdGVhbVBhY2tldBIoLkNSZW1vdGVDbGllbnRfU3Rl",
          "YW1Ub1N0ZWFtX05vdGlmaWNhdGlvbhoLLk5vUmVzcG9uc2UiPoK1GDpTZW5k",
          "IGEgcGFja2V0IHRvIGEgU3RlYW0gY2xpZW50IGZyb20gYSByZW1vdGUgU3Rl",
          "YW0gY2xpZW50EooBCh5Ob3RpZnlSZW1vdGVQbGF5VG9nZXRoZXJQYWNrZXQS",
          "IS5DUmVtb3RlUGxheVRvZ2V0aGVyX05vdGlmaWNhdGlvbhoLLk5vUmVzcG9u",
          "c2UiOIK1GDRTZW5kIGEgUmVtb3RlIFBsYXkgVG9nZXRoZXIgcGFja2V0IHRv",
          "IGEgU3RlYW0gY2xpZW50GjKCtRgqTWV0aG9kcyBmb3IgU3RlYW0gcmVtb3Rl",
          "IGNsaWVudCBvcGVyYXRpb25zwLUYAkIDgAEB"));
    descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
        new pbr::FileDescriptor[] { global::SteammessagesBaseReflection.Descriptor, global::SteammessagesUnifiedBaseSteamclientReflection.Descriptor, global::SteammessagesRemoteclientServiceMessagesReflection.Descriptor, },
        new pbr::GeneratedClrTypeInfo(null, null, null));
  }
  #endregion

}

#endregion Designer generated code
