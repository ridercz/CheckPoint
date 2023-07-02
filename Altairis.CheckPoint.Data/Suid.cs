using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;

namespace Altairis.CheckPoint.Data;

public struct Suid : IParsable<Suid>, IFormattable, IEquatable<Suid> {
    private const string SuidAlphabet = "0123456789ABCDEFGHJKMNPQRSTVWXYZ";
    private const int SuidLength = 12;

    private readonly string value;

    // Constructors

    public Suid() {
        this.value = new string(SuidAlphabet[0], SuidLength);
    }

    public Suid(string s) {
        if (string.IsNullOrWhiteSpace(s)) throw new ArgumentException("Value cannot be empty or whitespace only string.", nameof(s));

        s = new string(s.ToUpperInvariant().Where(SuidAlphabet.Contains).ToArray());
        if (s.Length != SuidLength) throw new FormatException("Value is not valid SUID.");

        this.value = s;

    }

    // Generating

    public static Suid NewSuid() {
        var chars = new char[SuidLength];
        for (var i = 0; i < SuidLength; i++) {
            chars[i] = SuidAlphabet[RandomNumberGenerator.GetInt32(SuidAlphabet.Length)];
        }
        return new(new string(chars));
    }

    // Parsing

    public static Suid Parse(string s, IFormatProvider? provider) => new(s);

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out Suid result) {
        if (s == null) {
            result = default;
            return false;
        }
        try {
            result = new(s);
            return true;
        } catch {
            result = default;
            return false;
        }
    }

    // Comparison

    public override readonly int GetHashCode() => this.value.GetHashCode();

    public static bool operator ==(Suid? left, Suid? right) => left?.Equals(right) ?? right is null;

    public static bool operator !=(Suid? left, Suid? right) => !(left == right);

    public readonly bool Equals(Suid other) => this.value.Equals(other.value, StringComparison.Ordinal);

    public override readonly bool Equals(object? obj) => obj is Suid suid && this.Equals(suid);

    // Formatting

    public override readonly string ToString() => this.ToString("G", null);

    public readonly string ToString(string? format, IFormatProvider? formatProvider) {
        return format switch {
            "n" => this.value.ToLowerInvariant(),
            "N" => this.value.ToUpperInvariant(),
            "g" => $"{this.value[0..3]}-{this.value[4..7]}-{this.value[8..12]}".ToLowerInvariant(),
            "G" or "" or null => $"{this.value[0..3]}-{this.value[4..7]}-{this.value[8..12]}".ToLowerInvariant(),
            _ => throw new FormatException($"Unknown format string '{format}'."),
        };
    }

    // Type conversion

    public static implicit operator string(Suid suid) => suid.ToString();

    public static implicit operator Suid(string s) => Parse(s, null);

}
