﻿using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;

namespace Altairis.CheckPoint.Data;

/// <summary>
/// Suid (String Unique IDentifier) is a human-friendly representation of random unique identifier. It's modelled after <see cref="Guid"/> class, but it's shorter and easier to read.
/// It uses Base32 encoding with alphabet that doesn't contain characters that are easily confused with each other (0 and O, 1 and I, etc.).
/// It is 16 characters long, which gives 80 bits of entropy, which is more than enough for most purposes.
/// Suid is usually formatted as four groups of four characters separated by dashes (xxxx-xxxx-xxxx-xxxx), but it can be formatted in other ways as well.
/// </summary>
public struct Suid : IParsable<Suid>, IFormattable, IEquatable<Suid> {
    public const string Alphabet = "0123456789ABCDEFGHJKMNPQRSTVWXYZ";
    public const int Length = 16;

    // Constructors

    public Suid(string s) {
        if (string.IsNullOrWhiteSpace(s)) throw new ArgumentException("Value cannot be empty or whitespace only string.", nameof(s));

        // Remove all non-alphabet characters
        s = new string(s.ToUpperInvariant().Where(Alphabet.Contains).ToArray());

        // Check if it's still valid value after removing non-alphabet characters
        if (s.Length != Length) throw new ArgumentException($"Value '{s}' is not valid SUID.", nameof(s));

        // Store value
        this.Value = s;
    }

    // Properties

    private string? value;

    public string Value {
        readonly get => this.value ?? new string(Alphabet[0], Length);
        init => this.value = value;
    }

    // Generating

    public static Suid NewSuid() {
        // Generate random string
        var chars = new char[Length];
        for (var i = 0; i < Length; i++) {
            chars[i] = Alphabet[RandomNumberGenerator.GetInt32(Alphabet.Length)];
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

    public override readonly int GetHashCode() => this.Value.GetHashCode();

    public static bool operator ==(Suid? left, Suid? right) => left?.Equals(right) ?? right is null;

    public static bool operator !=(Suid? left, Suid? right) => !(left == right);

    public readonly bool Equals(Suid other) => this.Value.Equals(other.Value, StringComparison.Ordinal);

    public override readonly bool Equals(object? obj) => obj is not null && obj is Suid suid && this.Equals(suid);

    // Formatting

    public override readonly string ToString() => this.ToString("G", null);

    public readonly string ToString(string? format) => this.ToString(format, null);

    public readonly string ToString(string? format, IFormatProvider? formatProvider) {
        // Use default format if none specified
        if (string.IsNullOrEmpty(format)) format = "G";

        // Apply format
        return format switch {
            // General format (xxxx-xxxx-xxxx-xxxx)
            "G" => $"{this.Value[0..4]}-{this.Value[4..8]}-{this.Value[8..12]}-{this.Value[12..16]}".ToUpperInvariant(),
            "g" => $"{this.Value[0..4]}-{this.Value[4..8]}-{this.Value[8..12]}-{this.Value[12..16]}".ToLowerInvariant(),
            // Space delimited format (xxxx xxxx xxxx xxxx)
            "S" => $"{this.Value[0..4]} {this.Value[4..8]} {this.Value[8..12]} {this.Value[12..16]}".ToUpperInvariant(),
            "s" => $"{this.Value[0..4]} {this.Value[4..8]} {this.Value[8..12]} {this.Value[12..16]}".ToLowerInvariant(),
            // No delimiters (xxxxxxxxxxxxxxxx)
            "N" => this.Value.ToUpperInvariant(),
            "n" => this.Value.ToLowerInvariant(),
            // Invalid format
            _ => throw new FormatException($"Unknown format string '{format}'."),
        };
    }

    // Type conversion

    public static implicit operator string(Suid suid) => suid.ToString();

    public static implicit operator Suid(string s) => Parse(s, null);

}